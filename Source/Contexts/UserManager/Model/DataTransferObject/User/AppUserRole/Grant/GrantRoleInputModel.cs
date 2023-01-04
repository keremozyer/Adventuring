namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;

/// <summary></summary>
public class GrantRoleInputModel
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