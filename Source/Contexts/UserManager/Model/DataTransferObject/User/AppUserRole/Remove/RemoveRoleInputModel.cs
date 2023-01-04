namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;

/// <summary></summary>
public class RemoveRoleInputModel
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