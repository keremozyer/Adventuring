using Adventuring.Architecture.AppException.Model.Derived.InvalidReferenceData;

namespace Adventuring.Architecture.Model.Interface.Validation;

/// <summary>
/// All setting classes that should be validated at application start must implement this interface.
/// </summary>
public interface IValidatableSetting
{
    /// <summary>
    /// Validates the instance and returns the validation result. Depending on the implementation may just throw the exception.
    /// </summary>
    /// <returns>Validation result. Will return <see langword="null"/> if validation is successful.</returns>
    public InvalidReferenceDataException? Validate();
}