using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Architecture.Application.Web.Concern.Option.OpenAPI;

/// <summary>
/// OpenAPI endpoint settings.
/// </summary>
public class EndpointSettings : IValidatableSetting
{
    /// <summary>
    /// Endpoint to host OpenAPI json data.
    /// </summary>
    public required string Endpoint { get; set; }
    /// <summary>
    /// API name.
    /// </summary>
    public required string APIName { get; set; }

    /// <summary>
    /// Validates the configuration values and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidReferenceDataException"></exception>
    public InvalidReferenceDataException? Validate()
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.Endpoint))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(EndpointSettings)}/{nameof(this.Endpoint)}"));
        }

        if (String.IsNullOrWhiteSpace(this.APIName))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(EndpointSettings)}/{nameof(this.APIName)}"));
        }

        return errors!.HasElements() ? throw new InvalidReferenceDataException(errors!) : null;
    }
}