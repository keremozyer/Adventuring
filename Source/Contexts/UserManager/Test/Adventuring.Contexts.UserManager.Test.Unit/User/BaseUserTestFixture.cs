using Adventuring.Contexts.UserManager.Concern.Option.AppUser;
using Adventuring.Contexts.UserManager.Mapper.Implementation.AutoMapperProfiles;
using Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.AppUser;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Test.Unit._Base;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Test.Unit.User;

public class BaseUserTestFixture : BaseUserManagerTestFixture
{
    protected readonly IUserMapper UserMapper = new UserMapper(new MapperConfiguration(configuration => configuration.AddProfile(new UserManagerAutoMapperProfile())).CreateMapper());
    protected readonly IUserRoleMapper UserRoleMapper = new UserRoleMapper(new MapperConfiguration(configuration => configuration.AddProfile(new UserManagerAutoMapperProfile())).CreateMapper());    
    protected readonly AppUserDefaults AppUserDefaults = new() { AdminRole = "Admin", DefaultRole = "Player" };
    protected readonly AppUserSettings AppUserSettings = new() { UngrantableRoles = new[] { "Admin" } };
}