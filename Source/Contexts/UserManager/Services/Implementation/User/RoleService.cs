using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Interface.User;

namespace Adventuring.Contexts.UserManager.Services.Implementation.User;

/// <inheritdoc/>
public class RoleService : IRoleService
{
    private readonly IRoleDataStore RoleDataStore;
    private readonly IRoleMapper RoleMapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="roleDataStore"></param>
    /// <param name="roleMapper"></param>
    public RoleService(IRoleDataStore roleDataStore, IRoleMapper roleMapper)
    {
        this.RoleDataStore = roleDataStore;
        this.RoleMapper = roleMapper;
    }

    /// <inheritdoc/>
    public async Task<CreateRoleOutputModel> Create(CreateRoleInputModel input)
    {
        ValidateInput(input);
        await ValidateData(input, this.RoleDataStore);

        Role role = new(input.Name);

        role.ID = await this.RoleDataStore.Create(role);

        return this.RoleMapper.Map(role);

        static void ValidateInput(CreateRoleInputModel input)
        {
            List<ValidationExceptionMessage>? errors = null;

            if (input is null)
            {
                throw new ValidationException(ErrorCodes.FieldCannotBeEmpty, nameof(CreateRoleInputModel));
            }

            if (String.IsNullOrWhiteSpace(input.Name))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(CreateRoleInputModel.Name)));
            }

            if (errors!.HasElements())
            {
                throw new ValidationException(errors!);
            }
        }

        static async Task ValidateData(CreateRoleInputModel input, IRoleDataStore roleDataStore)
        {
            Role? role = await roleDataStore.Get(input.Name);
            if (role is not null)
            {
                throw new BusinessException(ErrorCodes.RoleAlreadyExists);
            }
        }
    }

    /// <inheritdoc/>
    public async Task<GetRoleOutputModel> Get(string name)
    {
        Role role = await this.RoleDataStore.Get(name) ?? throw new DataNotFoundException(nameof(Role), name);

        return this.RoleMapper.MapGetRoleOutputModel(role);
    }
}