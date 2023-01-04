namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;

/// <summary></summary>
public class CreateUserInputModel
{
    /// <summary>
    /// User's identifier.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// User's password.
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// Initial roles to grant. If left null defualt role of 'Player' will be granted to the new user.
    /// </summary>
    public required IReadOnlyCollection<string> Roles { get; set; }
}