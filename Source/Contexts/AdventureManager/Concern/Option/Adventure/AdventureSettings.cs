using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;
using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Contexts.AdventureManager.Concern.Option.Adventure;

/// <summary></summary>
public class AdventureSettings : IOptionService, IValidatableSetting
{
    /// <summary>
    /// Role that can create adventures.
    /// </summary>
    public required string AdventureCreatorRole { get; set; }

    /// <summary>
    /// Validates the setting instance and returns the validation result without throwing it.
    /// </summary>
    /// <returns></returns>
    public InvalidReferenceDataException? Validate()
    {
        return String.IsNullOrWhiteSpace(this.AdventureCreatorRole) ? new InvalidReferenceDataException(ErrorCodes.InvalidConfigurationValue, $"{nameof(AdventureSettings)}/{nameof(this.AdventureCreatorRole)}") : null;
    }
}