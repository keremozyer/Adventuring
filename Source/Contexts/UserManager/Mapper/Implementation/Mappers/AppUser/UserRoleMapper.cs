using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.AppUser;

/// <inheritdoc/>
public class UserRoleMapper : IUserRoleMapper
{
    private readonly IMapper Mapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mapper"></param>
    public UserRoleMapper(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    /// <inheritdoc/>
    public GrantRoleInputModel Map(GrantRoleRequestModel model)
    {
        return this.Mapper.Map<GrantRoleInputModel>(model);
    }

    /// <inheritdoc/>
    public GrantRoleResponseModel Map(GrantRoleOutputModel model)
    {
        return this.Mapper.Map<GrantRoleResponseModel>(model);
    }

    /// <inheritdoc/>
    public RemoveRoleInputModel Map(RemoveRoleRequestModel model)
    {
        return this.Mapper.Map<RemoveRoleInputModel>(model);
    }

    /// <inheritdoc/>
    public RemoveRoleResponseModel Map(RemoveRoleOutputModel model)
    {
        return this.Mapper.Map<RemoveRoleResponseModel>(model);
    }
}