using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;

namespace Adventuring.Contexts.UserManager.Mapper.Interface.User;

/// <summary>
/// Maps related to the User model.
/// </summary>
public interface IUserRoleMapper : IMapperService
{
    /// <summary>
    /// Maps given <see cref="GrantRoleRequestModel"/> Contract to <see cref="GrantRoleInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GrantRoleInputModel Map(GrantRoleRequestModel model);
    /// <summary>
    /// Maps given <see cref="GrantRoleOutputModel"/> DTO to <see cref="GrantRoleResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GrantRoleResponseModel Map(GrantRoleOutputModel model);
    /// <summary>
    /// Maps given <see cref="RemoveRoleRequestModel"/> Contract to <see cref="RemoveRoleInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public RemoveRoleInputModel Map(RemoveRoleRequestModel model);
    /// <summary>
    /// Maps given <see cref="RemoveRoleOutputModel"/> DTO to <see cref="RemoveRoleResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public RemoveRoleResponseModel Map(RemoveRoleOutputModel model);
}