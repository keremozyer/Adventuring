namespace Adventuring.Architecture.Container.ActiveUser.Interface;

/// <summary>
/// Class to hold currently logged in user's data.
/// </summary>
public interface IActiveUser
{
    /// <summary>
    /// Logged in user's ID.
    /// </summary>
    public string? ID { get; }
    /// <summary>
    /// Logged in user's roles.
    /// </summary>
    public IReadOnlyCollection<string>? Roles { get; }
    /// <summary>
    /// Logged in user's name.
    /// </summary>
    public string? Username { get; }

    /// <summary>
    /// Returns <see langword="true"/> if user has any of the roles specified in <paramref name="roles"/>. Otherwise returns <see langword="false"/>.
    /// </summary>
    /// <param name="roles"></param>
    /// <returns></returns>
    bool HasRole(params string[] roles);
}