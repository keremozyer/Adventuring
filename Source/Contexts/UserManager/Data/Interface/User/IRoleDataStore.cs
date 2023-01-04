using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;

namespace Adventuring.Contexts.UserManager.Data.Interface.User;

/// <summary>
/// Persistent data layer for Role models.
/// </summary>
public interface IRoleDataStore : IDataStoreService
{
    /// <summary>
    /// Saves the role in the persistent data storage.
    /// </summary>
    /// <param name="role">Role to save.</param>
    /// <returns></returns>
    public Task<string> Create(Role role);
    /// <summary>
    /// Returns the role specified by this name if it exists. Otherwise returns <see langword="null"/>
    /// </summary>
    /// <param name="name">Name of the role.</param>
    /// <returns></returns>
    public Task<Role?> Get(string name);
}