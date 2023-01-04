using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;

namespace Adventuring.Contexts.UserManager.Mapper.Interface.User;

/// <summary>
/// Maps related to the Role model.
/// </summary>
public interface IRoleMapper : IMapperService
{
    /// <summary>
    /// Maps given <see cref="Model.Domain.UserAggregate.Role"/> Domain Model to <see cref="Model.Entity.User.Role"/> Entity.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Entity.User.Role MapRole(Model.Domain.UserAggregate.Role model);
    /// <summary>
    /// Maps given <see cref="Model.Entity.User.Role"/> Entity to <see cref="Model.Domain.UserAggregate.Role"/> Domain Model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Domain.UserAggregate.Role? MapRole(Model.Entity.User.Role? model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.UserAggregate.Role"/> Domain Model to <see cref="CreateRoleOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateRoleOutputModel Map(Model.Domain.UserAggregate.Role model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.UserAggregate.Role"/> Domain Model to <see cref="GetRoleOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GetRoleOutputModel MapGetRoleOutputModel(Model.Domain.UserAggregate.Role model);
}