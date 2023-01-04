using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;

namespace Adventuring.Architecture.Container.ActiveUser.Implementation.TestUser;

/// <summary>
/// Gets the user configured in tests.
/// </summary>
public class ActiveTestUser : IActiveUser
{
    /// <inheritdoc/>
    public string? ID { get; set; }
    /// <inheritdoc/>
    public IReadOnlyCollection<string>? Roles { get; set; }
    /// <inheritdoc/>
    public string? Username { get; set; }

    /// <summary></summary>
    /// <param name="id"></param>
    /// <param name="roles"></param>
    /// <param name="username"></param>
    public ActiveTestUser(string? id, IReadOnlyCollection<string>? roles, string? username)
    {
        this.ID = id;
        this.Roles = roles;
        this.Username = username;
    }

    /// <inheritdoc/>
    public bool HasRole(params string[] roles)
    {
        return this.Roles.IntersectSafe(roles)!.HasElements();
    }
}