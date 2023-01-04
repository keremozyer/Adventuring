using Adventuring.Architecture.AppException.Model.Derived.Validation;

namespace Adventuring.Architecture.Model.Interface.Validation;

/// <summary>
/// All data classes that should be validated at runtime must implement this interface.
/// </summary>
public interface IValidatableData
{
    /// <summary>
    /// Validates the instance and returns the validation result. Depending on the implementation may just throw the exception.
    /// </summary>
    /// <returns>Validation result. Will return <see langword="null"/> if validation is successful.</returns>
    public ValidationException? Validate();
}