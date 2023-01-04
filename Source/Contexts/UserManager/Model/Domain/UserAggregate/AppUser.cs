using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Model.Interface.Domain;
using Adventuring.Contexts.UserManager.Concern.Helper;

namespace Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;

/// <summary>
/// Represents a user registered to the system.
/// </summary>
public class AppUser : IAggregateRoot
{
    /// <summary>
    /// ID of the user. Will be assigned from the data persistence layer.
    /// </summary>
    public string ID { get; set; }
    /// <summary>
    /// Name of the user.
    /// </summary>
    public string Username { get; }
    /// <summary>
    /// Hashed password of the user.
    /// </summary>
    public string Password { get; }
    /// <summary>
    /// Salt for the password hash.
    /// </summary>
    public string Salt { get; }
    /// <summary>
    /// Roles of the user.
    /// </summary>
    public ICollection<Role> Roles { get; }

    /// <summary>
    /// Constructor to use when creating new users. Will hash the password given in <paramref name="password"/> before storing it in the instance.
    /// Will validate the instance by calling <see cref="Validate"/> and throw the result if there are any errors.
    /// </summary>
    /// <param name="username">Name of the user.</param>
    /// <param name="password">Plain text password of the user.</param>
    /// <param name="roles">Roles of the user.</param>
    public AppUser(string username, string password, ICollection<Role> roles)
    {
        this.Username = username;
        this.Password = password;
        this.Roles = roles;

        ValidationException? validationResult = Validate();
        if (validationResult is not null)
        {
            throw validationResult;
        }

        (this.Password, byte[] salt) = PasswordHelper.HashPassword(this.Password);
        this.Salt = Convert.ToBase64String(salt);
    }

    /// <summary>
    /// Constructor to use when reading an existing user to service layer.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="username">Name of the user.</param>
    /// <param name="password">Hashed password of the user.</param>
    /// <param name="salt">Salt for the password hash.</param>
    /// <param name="roles">Roles of the user.</param>
    public AppUser(string id, string username, string password, string salt, ICollection<Role> roles)
    {
        this.ID = id;
        this.Username = username;
        this.Password = password;
        this.Salt = salt;
        this.Roles = roles;
    }

    /// <summary>
    /// Validates the instance and returns the result without throwing it.
    /// </summary>
    /// <returns></returns>
    public ValidationException? Validate()
    {
        List<ValidationExceptionMessage>? errors = null;

        if (String.IsNullOrWhiteSpace(this.Username))
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.Username)));
        }

        if (String.IsNullOrWhiteSpace(this.Password))
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.Password)));
        }

        if (this.Roles.IsNullOrEmpty())
        {
            errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(this.Roles)));
        }

        return errors is null ? null : new ValidationException(errors);
    }
}