using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Contexts.UserManager.Concern.Option.AppUser;

/// <summary>
/// Default values for the user model.
/// </summary>
public class AppUserDefaults : IValidatableSetting, IOptionService
{
    /// <summary>
    /// Default role to grant when creating new users.
    /// </summary>
    public required string DefaultRole { get; set; }
    /// <summary>
    /// Administrator role's value.
    /// </summary>
    public required string AdminRole { get; set; }

    /// <summary>
    /// Validates the setting instance and returns the validation result without throwing it.
    /// </summary>
    /// <returns></returns>
    public InvalidReferenceDataException? Validate()
    {
        List<InvalidReferenceDataExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.DefaultRole))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(AppUserDefaults)}/{nameof(this.DefaultRole)}"));
        }

        if (String.IsNullOrWhiteSpace(this.AdminRole))
        {
            errors = errors.AddSafe(new InvalidReferenceDataExceptionMessage(ErrorCodes.ConfigurationValueCannotBeEmpty, $"{nameof(AppUserDefaults)}/{nameof(this.AdminRole)}"));
        }

        return errors!.HasElements() ? new InvalidReferenceDataException(errors!) : null;
    }
}