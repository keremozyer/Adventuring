namespace Adventuring.Contexts.UserManager.Model.Contract.Token.Create;

/// <summary>
/// Response contract for POST Token
/// </summary>
public class CreateTokenResponseModel
{
    /// <summary>
    /// Authentication token in JWT format.
    /// </summary>
    public required string Token { get; set; }
    /// <summary>
    /// Expiration date for the token, expressed in UTC timezone.
    /// </summary>
    public required DateTime ExpiresAt { get; set; }
    /// <summary>
    /// In how many seconds this token will expire.
    /// </summary>
    public required double ExpiresInSeconds { get; set; }
}