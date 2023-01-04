namespace Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Get;

/// <summary>
/// Response contract for GET AppUser/{ID}
/// </summary>
public class GetUserResponseModel
{
    /// <summary>
    /// User's name.
    /// </summary>
    public required string Username { get; set; }
}