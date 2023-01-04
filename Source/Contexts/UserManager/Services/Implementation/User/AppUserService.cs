using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.UserManager.Concern.Option.AppUser;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Interface.User;

namespace Adventuring.Contexts.UserManager.Services.Implementation.User;

/// <inheritdoc/>
public class AppUserService : IAppUserService
{
    private readonly IUserMapper UserMapper;
    private readonly IUserRoleMapper UserRoleMapper;
    private readonly IUserDataStore UserDataStore;
    private readonly IRoleDataStore RoleDataStore;
    private readonly AppUserDefaults AppUserDefaults;
    private readonly AppUserSettings AppUserSettings;
    private readonly IActiveUser ActiveUser;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="userMapper"></param>
    /// <param name="userDataStore"></param>
    /// <param name="roleDataStore"></param>
    /// <param name="appUserDefaults"></param>
    /// <param name="userRoleMapper"></param>
    /// <param name="appUserSettings"></param>
    /// <param name="activeUser"></param>
    public AppUserService(IUserMapper userMapper, IUserDataStore userDataStore, IRoleDataStore roleDataStore, AppUserDefaults appUserDefaults, IUserRoleMapper userRoleMapper, AppUserSettings appUserSettings, IActiveUser activeUser)
    {
        this.UserMapper = userMapper;
        this.UserDataStore = userDataStore;
        this.RoleDataStore = roleDataStore;
        this.AppUserDefaults = appUserDefaults;
        this.UserRoleMapper = userRoleMapper;
        this.AppUserSettings = appUserSettings;
        this.ActiveUser = activeUser;
    }

    /// <inheritdoc/>
    public async Task<CreateUserOutputModel> Create(CreateUserInputModel input)
    {
        ValidateInput(input);

        input.Roles ??= new string[] { this.AppUserDefaults.DefaultRole }; // We don't expect clients to send role data when creating new accounts. Initial roles are for inner flows.

        await ValidateData(this.RoleDataStore, this.UserDataStore, input);

        AppUser user = new(input.Username, input.Password, input.Roles.Select(role => new Role(role)).ToList());

        user.ID = await this.UserDataStore.Create(user);

        return this.UserMapper.Map(user);

        static void ValidateInput(CreateUserInputModel input)
        {
            List<ValidationExceptionMessage>? errors = null;

            if (input is null)
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(CreateUserInputModel)));
            }

            if (String.IsNullOrWhiteSpace(input?.Username))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(CreateUserInputModel.Username)));
            }

            if (String.IsNullOrWhiteSpace(input?.Password))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(CreateUserInputModel.Password)));
            }

            if (errors!.HasElements())
            {
                throw new ValidationException(errors!);
            }
        }

        static async Task ValidateData(IRoleDataStore roleDataStore, IUserDataStore userDataStore, CreateUserInputModel input)
        {
            foreach (string roleName in input.Roles)
            {
                Role? role = await roleDataStore.Get(roleName);
                if (role is null)
                {
                    throw new DataNotFoundException(nameof(Role), roleName);
                }
            }

            AppUser? user = await userDataStore.Get(input.Username);
            if (user is not null)
            {
                throw new BusinessException(ErrorCodes.UsernameAlreadyTaken);
            }
        }
    }

    /// <inheritdoc/>
    public async Task<GetUserOutputModel> Get(string username)
    {
        ValidateInput(username);

        AppUser user = await this.UserDataStore.Get(username) ?? throw new DataNotFoundException(nameof(AppUser), username);
        
        return this.UserMapper.MapGetUserOutputModel(user);

        static void ValidateInput(string username)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                throw new ValidationException(ErrorCodes.FieldCannotBeEmpty, nameof(username));
            }
        }
    }

    /// <inheritdoc/>
    public async Task<GrantRoleOutputModel> GrantRole(GrantRoleInputModel input)
    {
        if (!(this.ActiveUser?.HasRole(this.AppUserDefaults.AdminRole)).GetValueOrDefault()) // Only admins should be able to grant roles.
        {
            throw new UnauthorizedOperationException(nameof(GrantRole));
        }

        ValidateInput(input, this.AppUserSettings);

        AppUser user = await this.UserDataStore.Get(input.Username) ?? throw new DataNotFoundException(nameof(AppUser), input.Username);

        if (user.Roles.Any(role => role.Name == input.Role)) // If the user already have the role we can complete the operation successfuly.
        {
            return new GrantRoleOutputModel();
        }

        Role? roleToGrant = await this.RoleDataStore.Get(input.Role) ?? throw new DataNotFoundException(nameof(Role), input.Role);

        user.Roles.Add(roleToGrant);

        await this.UserDataStore.GrantRole(user, roleToGrant);

        return new GrantRoleOutputModel();

        static void ValidateInput(GrantRoleInputModel input, AppUserSettings appUserSettings)
        {
            List<BaseAppExceptionMessage>? errors = null;

            if (input is null)
            {
                throw new ValidationException(ErrorCodes.FieldCannotBeEmpty, nameof(GrantRoleInputModel));
            }

            if (String.IsNullOrWhiteSpace(input.Username))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(GrantRoleInputModel.Username)));
            }

            if (String.IsNullOrWhiteSpace(input.Role))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(GrantRoleInputModel.Role)));
            }

            if ((appUserSettings.UngrantableRoles?.Contains(input.Role)).GetValueOrDefault()) // Some roles should be reserved for system users and not granted to anyone else.
            {
                errors = errors.AddSafe(new BusinessExceptionMessage(ErrorCodes.RoleCannotBeGranted));
            }

            if (errors!.HasElements())
            {
                throw new BaseAppException(errors!);
            }
        }
    }

    /// <inheritdoc/>
    public async Task<RemoveRoleOutputModel> RemoveRole(RemoveRoleInputModel input)
    {
        if (!(this.ActiveUser?.HasRole(this.AppUserDefaults.AdminRole)).GetValueOrDefault()) // Only admins should be able to remove roles.
        {
            throw new UnauthorizedOperationException(nameof(RemoveRole));
        }

        ValidateInput(input, this.AppUserDefaults);

        AppUser user = await this.UserDataStore.Get(input.Username) ?? throw new DataNotFoundException(nameof(AppUser), input.Username);

        if (!user.Roles.Any(role => role.Name == input.Role)) // If the user already don't have the role we can complete the operation successfuly
        {
            return new RemoveRoleOutputModel();
        }

        Role? roleToRemove = await this.RoleDataStore.Get(input.Role) ?? throw new DataNotFoundException(nameof(Role), input.Role);

        user.Roles.RemoveBy(role => role.Name == input.Role);

        await this.UserDataStore.RemoveRole(user, roleToRemove);

        return new RemoveRoleOutputModel();

        static void ValidateInput(RemoveRoleInputModel input, AppUserDefaults appUserDefaults)
        {
            List<BaseAppExceptionMessage>? errors = null;

            if (input is null)
            {
                throw new ValidationException(ErrorCodes.FieldCannotBeEmpty, nameof(RemoveRoleInputModel));
            }

            if (String.IsNullOrWhiteSpace(input.Username))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(RemoveRoleInputModel.Username)));
            }

            if (String.IsNullOrWhiteSpace(input.Role))
            {
                errors = errors.AddSafe(new ValidationExceptionMessage(ErrorCodes.FieldCannotBeEmpty, nameof(RemoveRoleInputModel.Role)));
            }

            if (input.Role == appUserDefaults.DefaultRole)
            {
                errors = errors.AddSafe(new BusinessExceptionMessage(ErrorCodes.DefaultRoleCannotBeRemoved));
            }

            if (errors!.HasElements())
            {
                throw new BaseAppException(errors!);
            }
        }
    }
}