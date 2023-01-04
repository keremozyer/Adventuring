using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;

namespace Adventuring.Contexts.UserManager.Data.Implementation.User;

/// <inheritdoc/>
public class UserDataStore : IUserDataStore
{
    private readonly IRepository<Model.Entity.User.AppUser> AppUserRepository;
    private readonly IRepository<Model.Entity.User.Role> RoleRepository;
    private readonly IUserMapper UserMapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="userMapper"></param>
    /// <param name="repository"></param>
    /// <param name="roleRepository"></param>
    public UserDataStore(IUserMapper userMapper, IRepository<Model.Entity.User.AppUser> repository, IRepository<Model.Entity.User.Role> roleRepository)
    {
        this.AppUserRepository = repository;
        this.UserMapper = userMapper;
        this.RoleRepository = roleRepository;
    }

    /// <inheritdoc/>
    public async Task<string> Create(Model.Domain.UserAggregate.AppUser user)
    {
        Model.Entity.User.AppUser userEntity = this.UserMapper.MapAppUser(user);
        userEntity.Roles = await GetRoles(this.RoleRepository, user.Roles);

        _ = await this.AppUserRepository.Create(userEntity);

        return userEntity.ID;

        static async Task<List<Model.Entity.User.Role>> GetRoles(IRepository<Model.Entity.User.Role> roleRepository, IEnumerable<Model.Domain.UserAggregate.Role> roles)
        {
            List<DataNotFoundExceptionMessage>? errors = null;
            List<Model.Entity.User.Role> roleEntities = new();

            foreach (Model.Domain.UserAggregate.Role domainRole in roles)
            {
                Model.Entity.User.Role? role = await roleRepository.Get(role => role.Name == domainRole.Name);
                if (role is null)
                {
                    errors = errors.AddSafe(new DataNotFoundExceptionMessage(nameof(Model.Entity.User.Role), domainRole.Name));
                }
                else
                {
                    roleEntities.Add(role);
                }
            }

            return errors!.HasElements() ? throw new DataNotFoundException(errors!) : roleEntities;
        }
    }

    /// <inheritdoc/>
    public async Task<Model.Domain.UserAggregate.AppUser?> Get(string username)
    {
        Model.Entity.User.AppUser? userEntity = await this.AppUserRepository.Get(user => user.Username == username, includes: appUser => appUser.Roles);

        return this.UserMapper.MapAppUser(userEntity);
    }

    /// <inheritdoc/>
    public async Task GrantRole(Model.Domain.UserAggregate.AppUser user, Model.Domain.UserAggregate.Role roleToGrant)
    {
        Model.Entity.User.AppUser? userEntity = await this.AppUserRepository.Get(x => x.Username == user.Username, includes: x => x.Roles);
        Model.Entity.User.Role? roleEntity = await this.RoleRepository.Get(x => x.Name == roleToGrant.Name);

        userEntity!.Roles.Add(roleEntity!);
    }

    /// <inheritdoc/>
    public async Task RemoveRole(Model.Domain.UserAggregate.AppUser user, Model.Domain.UserAggregate.Role roleToRemove)
    {
        Model.Entity.User.AppUser? userEntity = await this.AppUserRepository.Get(x => x.Username == user.Username, includes: x => x.Roles);
        Model.Entity.User.Role? roleEntity = await this.RoleRepository.Get(x => x.Name == roleToRemove.Name);

        _ = userEntity!.Roles.Remove(roleEntity!);
    }
}