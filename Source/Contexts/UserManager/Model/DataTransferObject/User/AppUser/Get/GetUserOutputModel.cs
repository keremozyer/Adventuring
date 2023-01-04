namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;

/// <summary></summary>
public class GetUserOutputModel
{
    /// <summary>
    /// ID of the user.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// User's identifier.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Hashed password of the user.
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// Salt for the password.
    /// </summary>
    public required string Salt { get; set; }
    /// <summary>
    /// Roles of the user.
    /// </summary>
    public required IReadOnlyCollection<string> Roles { get; set; }
}