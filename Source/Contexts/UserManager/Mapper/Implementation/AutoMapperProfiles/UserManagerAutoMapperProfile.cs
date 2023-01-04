using Adventuring.Contexts.UserManager.Model.Contract.Token.Create;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Mapper.Implementation.AutoMapperProfiles;

/// <summary>
/// AutoMapper profile for the application.
/// </summary>
public class UserManagerAutoMapperProfile : Profile
{
    /// <summary></summary>
    public UserManagerAutoMapperProfile()
    {
        CreateRoleMaps();
        CreateUserMaps();
        CreateTokenMaps();
        CreateUserRoleMaps();
    }

    private void CreateRoleMaps()
    {
        _ = CreateMap<Model.Domain.UserAggregate.Role, Model.Entity.User.Role>()
            .ForMember(entity => entity.ID, option => option.Ignore());
        _ = CreateMap<Model.Entity.User.Role, Model.Domain.UserAggregate.Role>()
            .ConstructUsing(entity => new Model.Domain.UserAggregate.Role(entity.ID, entity.Name));
        _ = CreateMap<Model.Domain.UserAggregate.Role, CreateRoleOutputModel>();
        _ = CreateMap<Model.Domain.UserAggregate.Role, GetRoleOutputModel>();
    }

    private void CreateUserMaps()
    {
        _ = CreateMap<CreateUserRequestModel, CreateUserInputModel>();
        _ = CreateMap<CreateUserOutputModel, CreateUserResponseModel>();
        _ = CreateMap<Model.Domain.UserAggregate.AppUser, CreateUserOutputModel>();
        _ = CreateMap<Model.Domain.UserAggregate.AppUser, GetUserOutputModel>()
            .ForMember(dto => dto.Roles, config => config.MapFrom(domain => domain.Roles.Select(role => role.Name)));
        _ = CreateMap<GetUserOutputModel, GetUserResponseModel>();
        _ = CreateMap<Model.Domain.UserAggregate.AppUser, Model.Entity.User.AppUser>()
            .ForMember(entity => entity.ID, option => option.Ignore());
        _ = CreateMap<Model.Entity.User.AppUser, Model.Domain.UserAggregate.AppUser>()
            .ConstructUsing(entity => new Model.Domain.UserAggregate.AppUser(entity.ID, entity.Username, entity.Password, entity.Salt, entity.Roles.Select(role => new Model.Domain.UserAggregate.Role(entity.ID, role.Name)).ToList()));
    }

    private void CreateTokenMaps()
    {
        _ = CreateMap<CreateTokenRequestModel, CreateTokenInputModel>();
        _ = CreateMap<CreateTokenOutputModel, CreateTokenResponseModel>();
    }

    private void CreateUserRoleMaps()
    {
        _ = CreateMap<GrantRoleRequestModel, GrantRoleInputModel>();
        _ = CreateMap<GrantRoleOutputModel, GrantRoleResponseModel>();
        _ = CreateMap<RemoveRoleRequestModel, RemoveRoleInputModel>();
        _ = CreateMap<RemoveRoleOutputModel, RemoveRoleResponseModel>();
    }
}