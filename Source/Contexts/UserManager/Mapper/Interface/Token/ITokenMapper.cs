using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.Contract.Token.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;

namespace Adventuring.Contexts.UserManager.Mapper.Interface.Token;

/// <summary>
/// Maps related to the Token model.
/// </summary>
public interface ITokenMapper : IMapperService
{
    /// <summary>
    /// Maps given <see cref="CreateTokenRequestModel"/> Contract to <see cref="CreateTokenInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateTokenInputModel Map(CreateTokenRequestModel model);
    /// <summary>
    /// Maps given <see cref="CreateTokenOutputModel"/> DTO to <see cref="CreateTokenResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateTokenResponseModel Map(CreateTokenOutputModel model);
}