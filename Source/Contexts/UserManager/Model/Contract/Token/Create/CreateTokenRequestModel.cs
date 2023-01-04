namespace Adventuring.Contexts.UserManager.Model.Contract.Token.Create;

/// <summary></summary>
public class CreateTokenRequestModel
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Password.
    /// </summary>
    public required string Password { get; set; }
}