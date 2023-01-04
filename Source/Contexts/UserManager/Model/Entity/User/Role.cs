using Adventuring.Architecture.Model.Entity.Implementation.Record;

namespace Adventuring.Contexts.UserManager.Model.Entity.User;

/// <summary>
/// Persistent data record model for Role.
/// </summary>
public class Role : BaseSoftDeletedRecord
{
    /// <summary>
    /// Role's name.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Navigation for AppUser entity.
    /// </summary>
    public required virtual ICollection<AppUser> AppUsers { get; set; }
}