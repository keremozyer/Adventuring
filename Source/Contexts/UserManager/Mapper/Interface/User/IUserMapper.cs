using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;

namespace Adventuring.Contexts.UserManager.Mapper.Interface.User;

/// <summary>
/// Mappers related to User model.
/// </summary>
public interface IUserMapper : IMapperService
{
    /// <summary>
    /// Maps given <see cref="CreateUserRequestModel"/> Contract to <see cref="CreateUserInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateUserInputModel Map(CreateUserRequestModel model);
    /// <summary>
    /// Maps given <see cref="CreateUserOutputModel"/> DTO to <see cref="CreateUserResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateUserResponseModel Map(CreateUserOutputModel model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.UserAggregate.AppUser"/> Domain model to <see cref="CreateUserOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public CreateUserOutputModel Map(Model.Domain.UserAggregate.AppUser model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.UserAggregate.AppUser"/> Domain model to <see cref="GetUserOutputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GetUserOutputModel MapGetUserOutputModel(Model.Domain.UserAggregate.AppUser model);
    /// <summary>
    /// Maps given <see cref="GetUserOutputModel"/> DTO to <see cref="GetUserResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GetUserResponseModel MapGetUserOutputModel(GetUserOutputModel model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.UserAggregate.AppUser"/> Domain model to <see cref="Model.Entity.User.AppUser"/> Entity.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Entity.User.AppUser MapAppUser(Model.Domain.UserAggregate.AppUser model);
    /// <summary>
    /// Maps given <see cref="Model.Entity.User.AppUser"/> Entity to <see cref="Model.Domain.UserAggregate.AppUser"/> Domain model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Domain.UserAggregate.AppUser? MapAppUser(Model.Entity.User.AppUser? model);
}