using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Architecture.Application.Web.Concern.Option.OpenAPI;

/// <summary>
/// OpenAPI settings
/// </summary>
public class OpenAPISettings : IValidatableSetting
{
    /// <summary>
    /// Title of the application.
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// If set to <see langword="true"/> will configure security definitions for the OpenAPI UI.
    /// </summary>
    public required bool? HasSecurity { get; set; }
    /// <summary>
    /// Settings for OpenAPI definition endpoints.
    /// </summary>
    public required IReadOnlyCollection<EndpointSettings> EndpointSettings { get; set; }

    /// <summary>
    /// Will validate the instance and return the result without throwing it.
    /// </summary>
    /// <returns></returns>
    public InvalidReferenceDataException? Validate()
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.Title))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(OpenAPISettings)}/{nameof(this.Title)}"));
        }

        if (this.HasSecurity is null)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(OpenAPISettings)}/{nameof(this.HasSecurity)}"));
        }

        if (this.EndpointSettings.IsNullOrEmpty())
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(OpenAPISettings)}/{nameof(this.EndpointSettings)}"));
        }

        foreach (EndpointSettings endpointSettings in this.EndpointSettings ?? Array.Empty<EndpointSettings>())
        {
            errors = errors.AddRangeSafe(endpointSettings.Validate()?.Messages?.Cast<InvalidReferenceDataExceptionMessage>());
        }

        return errors!.HasElements() ? new InvalidReferenceDataException(errors!) : null;
    }
}