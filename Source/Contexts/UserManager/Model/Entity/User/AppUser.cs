using Adventuring.Architecture.Model.Entity.Implementation.Record;

namespace Adventuring.Contexts.UserManager.Model.Entity.User;

/// <summary>
/// Persistent data record model for User.
/// </summary>
public class AppUser : BaseSoftDeletedRecord
{
    /// <summary></summary>
    public required string Username { get; set; }
    /// <summary>
    /// Hashed password.
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// Salt for password hash.
    /// </summary>
    public required string Salt { get; set; }
    /// <summary>
    /// Navigation for Role entity.
    /// </summary>
    public required virtual ICollection<Role> Roles { get; set; }
}