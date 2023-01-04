using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;

namespace Adventuring.Contexts.UserManager.Data.Implementation.User;

/// <inheritdoc/>
public class RoleDataStore : IRoleDataStore
{
    private readonly IRepository<Model.Entity.User.Role> Repository;
    private readonly IRoleMapper RoleMapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="roleMapper"></param>
    public RoleDataStore(IRepository<Model.Entity.User.Role> repository, IRoleMapper roleMapper)
    {
        this.Repository = repository;
        this.RoleMapper = roleMapper;
    }

    /// <inheritdoc/>
    public async Task<string> Create(Model.Domain.UserAggregate.Role role)
    {
        Model.Entity.User.Role roleEntity = this.RoleMapper.MapRole(role);
        return await this.Repository.Create(roleEntity);
    }

    /// <inheritdoc/>
    public async Task<Model.Domain.UserAggregate.Role?> Get(string name)
    {
        Model.Entity.User.Role? role = await this.Repository.Get(role => role.Name == name);

        return this.RoleMapper.MapRole(role);
    }
}