using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;

namespace Adventuring.Contexts.UserManager.Services.Interface.User;

/// <summary>
/// Business flows for app user model.
/// </summary>
public interface IAppUserService : IBusinessService
{
    /// <summary>
    /// Creates a new user with given parameters. If <see cref="CreateUserInputModel.Roles"/> is left empty the default role of Player will be assigned.
    /// Will throw a BusinessException with UsernameAlreadyTaken error code if a user with username specified in the <see cref="CreateUserInputModel.Username"/> parameter already exists.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<CreateUserOutputModel> Create(CreateUserInputModel input);
    /// <summary>
    /// Returns the user specified by this username. Will throw a DataNotFoundException if a user is not found.
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public Task<GetUserOutputModel> Get(string username);
    /// <summary>
    /// Will grant the specified role to the specified user. If the user already have the role operation will be completed successfuly.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<GrantRoleOutputModel> GrantRole(GrantRoleInputModel input);
    /// <summary>
    /// Will remove the specified role from the specified user. If the user already don't have the role operation will be completed successfuly.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<RemoveRoleOutputModel> RemoveRole(RemoveRoleInputModel input);
}