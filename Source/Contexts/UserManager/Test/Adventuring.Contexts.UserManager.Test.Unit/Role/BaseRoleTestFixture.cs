using Adventuring.Contexts.UserManager.Mapper.Implementation.AutoMapperProfiles;
using Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.AppUser;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Test.Unit._Base;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Test.Unit._Role;

public class BaseRoleTestFixture : BaseUserManagerTestFixture
{
    protected readonly IRoleMapper RoleMapper = new RoleMapper(new MapperConfiguration(configuration => configuration.AddProfile(new UserManagerAutoMapperProfile())).CreateMapper());
}