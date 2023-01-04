using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.AppUser;

/// <inheritdoc/>
public class UserMapper : IUserMapper
{
    private readonly IMapper Mapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mapper"></param>
    public UserMapper(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    /// <inheritdoc/>
    public CreateUserInputModel Map(CreateUserRequestModel model)
    {
        return this.Mapper.Map<CreateUserInputModel>(model);
    }

    /// <inheritdoc/>
    public CreateUserResponseModel Map(CreateUserOutputModel model)
    {
        return this.Mapper.Map<CreateUserResponseModel>(model);
    }

    /// <inheritdoc/>
    public CreateUserOutputModel Map(Model.Domain.UserAggregate.AppUser model)
    {
        return this.Mapper.Map<CreateUserOutputModel>(model);
    }

    /// <inheritdoc/>
    public Model.Entity.User.AppUser MapAppUser(Model.Domain.UserAggregate.AppUser model)
    {
        return this.Mapper.Map<Model.Entity.User.AppUser>(model);
    }

    /// <inheritdoc/>
    public Model.Domain.UserAggregate.AppUser? MapAppUser(Model.Entity.User.AppUser? model)
    {
        return this.Mapper.Map<Model.Domain.UserAggregate.AppUser?>(model);
    }

    /// <inheritdoc/>
    public GetUserOutputModel MapGetUserOutputModel(Model.Domain.UserAggregate.AppUser model)
    {
        return this.Mapper.Map<GetUserOutputModel>(model);
    }

    /// <inheritdoc/>
    public GetUserResponseModel MapGetUserOutputModel(GetUserOutputModel model)
    {
        return this.Mapper.Map<GetUserResponseModel>(model);
    }
}