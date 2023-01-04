namespace Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Remove;

/// <summary>
/// Request contract for DELETE AppUserRole
/// </summary>
public class RemoveRoleRequestModel
{
    /// <summary>
    /// Name of the user to remove the role from.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Role to remove.
    /// </summary>
    public required string Role { get; set; }
}