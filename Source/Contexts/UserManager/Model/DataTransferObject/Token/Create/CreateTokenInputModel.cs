namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;

/// <summary></summary>
public class CreateTokenInputModel
{
    /// <summary>
    /// User's name.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// User's password.
    /// </summary>
    public required string Password { get; set; }
}