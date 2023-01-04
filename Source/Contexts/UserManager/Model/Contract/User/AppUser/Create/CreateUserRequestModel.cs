namespace Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Create;

/// <summary>
/// Request contract for POST AppUser
/// </summary>
public class CreateUserRequestModel
{
    /// <summary>
    /// User's identifier.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// User's password.
    /// </summary>
    public required string Password { get; set; }
}