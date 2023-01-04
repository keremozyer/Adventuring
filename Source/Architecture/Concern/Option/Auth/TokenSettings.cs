using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Architecture.Concern.Option.Auth;

/// <summary>
/// Authentication token settings.
/// </summary>
public class TokenSettings : IValidatableSetting, IOptionService
{
    /// <summary>
    /// Key to use when signing the token.
    /// </summary>
    public required string SecurityKey { get; set; }
    /// <summary>
    /// Expiration time in seconds for the token.
    /// </summary>
    public required int ExpiresInSeconds { get; set; }

    /// <summary>
    /// Validates the instance and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    public InvalidReferenceDataException? Validate()
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.SecurityKey))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(TokenSettings)}/{nameof(this.SecurityKey)}"));
        }

        if (this.ExpiresInSeconds <= 0)
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.InvalidConfigurationValue, $"{nameof(TokenSettings)}/{nameof(this.ExpiresInSeconds)}"));
        }

        return errors is null ? null : new InvalidReferenceDataException(errors);
    }
}