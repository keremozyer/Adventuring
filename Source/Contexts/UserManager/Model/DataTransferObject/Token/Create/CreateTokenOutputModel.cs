namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;

/// <summary></summary>
public class CreateTokenOutputModel
{
    /// <summary>
    /// Authentication token in JWT format.
    /// </summary>
    public string Token { get; }
    /// <summary>
    /// Expiration date for the token, expressed in UTC timezone.
    /// </summary>
    public DateTime ExpiresAt { get; }
    /// <summary>
    /// In how many seconds this token will expire.
    /// </summary>
    public double ExpiresInSeconds { get; }

    /// <summary></summary>
    /// <param name="token">Authentication token in JWT format.</param>
    /// <param name="expiresAt">Expiration date for the token, expressed in UTC timezone.</param>
    /// <param name="expiresInSeconds">In how many seconds this token will expire.</param>
    public CreateTokenOutputModel(string token, DateTime expiresAt, double expiresInSeconds)
    {
        this.Token = token;
        this.ExpiresAt = expiresAt;
        this.ExpiresInSeconds = expiresInSeconds;
    }
}