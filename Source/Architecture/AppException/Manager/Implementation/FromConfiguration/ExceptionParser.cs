using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Manager.Interface;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.AppException.Model.Parsed;
using Adventuring.Architecture.Concern.Constant.Logging;
using Adventuring.Architecture.Concern.Extension;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Adventuring.Architecture.AppException.Manager.Implementation.FromConfiguration;

/// <summary>
/// Class to parse handled and unhandled exceptions to user-friendly format.
/// </summary>
public class ExceptionParser : IExceptionParser
{
    private readonly ILogger<ExceptionParser> Logger;
    private readonly IConfiguration Configuration;

    private readonly Dictionary<string, string> MessageTemplates;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public ExceptionParser(ILogger<ExceptionParser> logger, IConfiguration configuration)
    {
        this.Logger = logger;
        this.Configuration = configuration;

        this.MessageTemplates = this.Configuration!.GetSection(nameof(this.MessageTemplates)).GetChildren().ToDictionary(code => code.Key, code => code.Value)!;
    }

    /// <summary>
    /// If the given <paramref name="exception"/> is a custom exception it's code will be converted to a user-friendly message and a <see cref="ParsedException"/> instance will be returned containing this message and an appropriate <see cref="HttpStatusCode"/>.
    /// If it's anything other than a custom exception it will be treated as an unhandled exception and a <see cref="ParsedException"/> instance will be returned containing a generic message with <see cref="HttpStatusCode.InternalServerError"/> code. <see cref="ParsedException.LogID"/> property will be populated with a random Guid represented as a string to inform the caller that this error needs to be logged.
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public ParsedException Parse(Exception exception)
    {
        return exception switch
        {
            BusinessException businessException => ParseBusinessException(businessException),
            DataNotFoundException dataNotFoundException => ParseDataNotFoundException(dataNotFoundException),
            InvalidReferenceDataException invalidReferenceDataException => ParseInvalidReferenceDataException(invalidReferenceDataException),
            UnauthorizedOperationException unauthorizedOperationException => ParseUnauthorizedOperationException(unauthorizedOperationException),
            ValidationException validationException => ParseValidationException(validationException),
            _ => ParseUnhandledException()
        };
    }

    private string ApplyMessageTemplate(ErrorCodes code, params string[] values)
    {
        if (!this.MessageTemplates.TryGetValue(code.ToString(), out string? messageTemplate))
        {
            this.Logger.LogWarning(LogEvents.ExceptionMessageTemplateNotFound, LogTemplates.ExceptionMessageTemplateNotFound, code);
        }

        messageTemplate ??= this.MessageTemplates[ErrorCodes.UnexpectedError.ToString()];

        return values!.HasElements() ? String.Format(messageTemplate, values) : messageTemplate;
    }

    private ParsedException ParseBusinessException(BusinessException businessException)
    {
        return new ParsedException(HttpStatusCode.UnprocessableEntity, null, businessException.Messages?.Select(message => new ParsedExceptionMessage(message.Code, String.IsNullOrWhiteSpace(message.Message) ? ApplyMessageTemplate(message.Code) : message.Message))?.ToArray());
    }

    private ParsedException ParseDataNotFoundException(DataNotFoundException dataNotFoundException)
    {
        return new ParsedException(HttpStatusCode.BadRequest, null, dataNotFoundException.Messages?.Select(message =>
        {
            DataNotFoundExceptionMessage dataNotFoundExceptionMessage = (message as DataNotFoundExceptionMessage)!;
            return new ParsedExceptionMessage(dataNotFoundExceptionMessage.Code, String.IsNullOrWhiteSpace(dataNotFoundExceptionMessage.Message) ? ApplyMessageTemplate(dataNotFoundExceptionMessage.Code, dataNotFoundExceptionMessage.SearchedEntity, dataNotFoundExceptionMessage.SearchValue) : dataNotFoundExceptionMessage.Message);
        })?.ToArray());
    }

    private ParsedException ParseInvalidReferenceDataException(InvalidReferenceDataException invalidReferenceDataException)
    {
        return new ParsedException(HttpStatusCode.InternalServerError, Guid.NewGuid().ToString(), invalidReferenceDataException.Messages?.Select(message =>
        {
            InvalidReferenceDataExceptionMessage invalidReferenceDataExceptionMessage = (message as InvalidReferenceDataExceptionMessage)!;
            return new ParsedExceptionMessage(invalidReferenceDataExceptionMessage.Code, String.IsNullOrWhiteSpace(invalidReferenceDataExceptionMessage.Message) ? ApplyMessageTemplate(invalidReferenceDataExceptionMessage.Code, invalidReferenceDataExceptionMessage.ReferenceDataName) : invalidReferenceDataExceptionMessage.Message);
        })?.ToArray());
    }

    private ParsedException ParseUnauthorizedOperationException(UnauthorizedOperationException unauthorizedOperationException)
    {
        return new ParsedException(HttpStatusCode.InternalServerError, Guid.NewGuid().ToString(), unauthorizedOperationException.Messages?.Select(message =>
        {
            UnauthorizedOperationExceptionMessage unauthorizedOperationExceptionMessage = (message as UnauthorizedOperationExceptionMessage)!;
            return new ParsedExceptionMessage(unauthorizedOperationExceptionMessage.Code, String.IsNullOrWhiteSpace(unauthorizedOperationExceptionMessage.Message) ? ApplyMessageTemplate(unauthorizedOperationExceptionMessage.Code, unauthorizedOperationExceptionMessage.OperationName) : unauthorizedOperationExceptionMessage.Message);
        })?.ToArray());
    }

    private ParsedException ParseValidationException(ValidationException validationException)
    {
        return new(HttpStatusCode.BadRequest, null, validationException.Messages?.Select(message =>
        {
            ValidationExceptionMessage validationMessage = (message as ValidationExceptionMessage)!;
            return new ParsedExceptionMessage(validationMessage.Code, String.IsNullOrWhiteSpace(validationMessage.Message) ? ApplyMessageTemplate(validationMessage.Code, new string[] { validationMessage.FieldName }.ConcatSafe(validationMessage.ExtraInfo)?.ToArray()!) : validationMessage.Message);
        })?.ToArray());
    }

    private ParsedException ParseUnhandledException()
    {
        return new(HttpStatusCode.InternalServerError, Guid.NewGuid().ToString(), new ParsedExceptionMessage(ErrorCodes.UnexpectedError, ApplyMessageTemplate(ErrorCodes.UnexpectedError)));
    }
}