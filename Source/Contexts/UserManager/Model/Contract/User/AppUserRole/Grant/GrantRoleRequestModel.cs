namespace Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Grant;

/// <summary>
/// Request contract for POST AppUserRole
/// </summary>
public class GrantRoleRequestModel
{
    /// <summary>
    /// User's name to grant the role to.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Role to grant.
    /// </summary>
    public required string Role { get; set; }
}