namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;

/// <summary></summary>
public class CreateRoleInputModel
{
    /// <summary>
    /// Name of the new role.
    /// </summary>
    public required string Name { get; set; }
}