using Adventuring.Architecture.Model.Interface.Validation;

namespace Adventuring.Architecture.Model.Interface.Domain;

/// <summary>
/// All domain objects must implement this interface.
/// </summary>
public interface IDomainObject : IValidatableData
{

}