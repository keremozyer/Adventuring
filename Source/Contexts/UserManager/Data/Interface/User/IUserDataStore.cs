using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;

namespace Adventuring.Contexts.UserManager.Data.Interface.User;

/// <summary>
/// Persistent data layer for User models.
/// </summary>
public interface IUserDataStore : IDataStoreService
{
    /// <summary>
    /// Saves the user in the persistent data storage.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task<string> Create(AppUser user);
    /// <summary>
    /// Returns the user specified by this username if it exists. Otherwise returns <see langword="null"/>
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public Task<AppUser?> Get(string username);
    /// <summary>
    /// Updates the user's roles with <paramref name="roleToGrant"/>.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleToGrant"></param>
    /// <returns></returns>
    public Task GrantRole(AppUser user, Role roleToGrant);
    /// <summary>
    /// Removes the <paramref name="roleToRemove"/> from the user's roles.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleToRemove"></param>
    /// <returns></returns>
    public Task RemoveRole(AppUser user, Role roleToRemove);
}