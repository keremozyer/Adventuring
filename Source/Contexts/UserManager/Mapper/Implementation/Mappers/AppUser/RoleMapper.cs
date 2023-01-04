using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;
using AutoMapper;

namespace Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.AppUser;

/// <inheritdoc/>
public class RoleMapper : IRoleMapper
{
    private readonly IMapper Mapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mapper"></param>
    public RoleMapper(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    /// <inheritdoc/>
    public CreateRoleOutputModel Map(Model.Domain.UserAggregate.Role model)
    {
        return this.Mapper.Map<CreateRoleOutputModel>(model);
    }

    /// <inheritdoc/>
    public GetRoleOutputModel MapGetRoleOutputModel(Model.Domain.UserAggregate.Role model)
    {
        return this.Mapper.Map<GetRoleOutputModel>(model);
    }

    /// <inheritdoc/>
    public Model.Entity.User.Role MapRole(Model.Domain.UserAggregate.Role model)
    {
        return this.Mapper.Map<Model.Entity.User.Role>(model);
    }

    /// <inheritdoc/>
    public Model.Domain.UserAggregate.Role? MapRole(Model.Entity.User.Role? model)
    {
        return this.Mapper.Map<Model.Domain.UserAggregate.Role>(model);
    }
}