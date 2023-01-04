using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;

namespace Adventuring.Contexts.UserManager.Services.Interface.Token;

/// <summary>
/// Business flows for authentication token.
/// </summary>
public interface ITokenService : IBusinessService
{
    /// <summary>
    /// Creates a new authenticaton token for the user given.
    /// Will throw a DataNotFoundException if the user is not found.
    /// Will throw a BusinessException with InvalidPassword error code if the provided password does not match the password stored for the user.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<CreateTokenOutputModel> Create(CreateTokenInputModel input);
}