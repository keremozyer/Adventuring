using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;

namespace Adventuring.Contexts.UserManager.Services.Interface.User;

/// <summary>
/// Business flows for role model.
/// </summary>
public interface IRoleService : IBusinessService
{
    /// <summary>
    /// Creates a new role with given parameters.
    /// Will throw a BusinessException with RoleAlreadyExists error code if the role already exists.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<CreateRoleOutputModel> Create(CreateRoleInputModel input);
    /// <summary>
    /// Returns the role specified by this name.
    /// Will throw a DataNotfoundException if a role with the name of <paramref name="name"/> is not found.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public Task<GetRoleOutputModel> Get(string name);
}