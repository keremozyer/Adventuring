namespace Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Create;

/// <summary>
/// Response contract for POST AppUser
/// </summary>
public class CreateUserResponseModel
{
    /// <summary>
    /// ID of the newly created user.
    /// </summary>
    public required string ID { get; set; }
}