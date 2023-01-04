using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Model.Interface.Domain;

namespace Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;

/// <summary>
/// Represents a user role.
/// </summary>
public class Role : IDomainObject
{
    /// <summary>
    /// ID of the role.
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    /// Name of the role.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Constructor to use when creating new roles.
    /// Will validate the instance and throw the result if there are any errors.
    /// </summary>
    /// <param name="name">Name of the role.</param>
    public Role(string name)
    {
        this.Name = name;

        ValidationException? validationResult = Validate();
        if (validationResult is not null)
        {
            throw validationResult;
        }
    }

    /// <summary>
    /// Constructor to use when reading an existing role to service layer.
    /// </summary>
    /// <param name="id">ID of the role.</param>
    /// <param name="name">Name of the role.</param>
    public Role(string id, string name)
    {
        this.ID = id;
        this.Name = name;
    }

    /// <summary>
    /// Validates the instance and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public ValidationException? Validate()
    {
        return String.IsNullOrWhiteSpace(this.Name) ? throw new ValidationException(ErrorCodes.FieldCannotBeEmpty, nameof(this.Name)) : null;
    }
}