namespace Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;

/// <summary></summary>
public class GetRoleOutputModel
{
    /// <summary>
    /// ID of the role.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// Name of the role.
    /// </summary>
    public required string Name { get; set; }
}