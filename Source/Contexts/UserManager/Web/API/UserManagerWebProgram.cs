using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.Application.Web.Core;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Contexts.UserManager.Concern.Option.AppUser;
using Adventuring.Contexts.UserManager.Concern.Option.DataSeed;
using Adventuring.Contexts.UserManager.Data.Implementation.User;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Context;
using Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Repositories;
using Adventuring.Contexts.UserManager.Mapper.Implementation.AutoMapperProfiles;
using Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.AppUser;
using Adventuring.Contexts.UserManager.Mapper.Implementation.Mappers.Token;
using Adventuring.Contexts.UserManager.Mapper.Interface.Token;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Services.Implementation.Token;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Adventuring.Contexts.UserManager.Services.Interface.Token;
using Adventuring.Contexts.UserManager.Services.Interface.User;

namespace Adventuring.Contexts.UserManager.Web.API;

/// <summary>
/// Web program initializer.
/// </summary>
public class UserManagerWebProgram : BaseWebProgram
{
    /// <summary></summary>
    /// <param name="arguments">Application start arguments.</param>
    public UserManagerWebProgram(string[] arguments) : base(GetConfigurationOptions(arguments))
    {
        base.CheckDatabase<UserManagerDataContext>().Wait();
    }

    private static WebProgramConfigurationOptions GetConfigurationOptions(string[] arguments)
    {
        return new WebProgramConfigurationOptions(arguments)
        {
            AutoMapperProfile = new UserManagerAutoMapperProfile()
        };
    }

    /// <inheritdoc/>
    protected override void ConfigureOptions()
    {
        base.ConfigureOption<DataSeedSettings>();
        base.ConfigureOption<AppUserDefaults>();
        base.ConfigureOption<AppUserSettings>();
    }

    /// <inheritdoc/>
    protected override void ConfigureMappers()
    {
        base.ConfigureMapper<IRoleMapper, RoleMapper>();
        base.ConfigureMapper<IUserMapper, UserMapper>();
        base.ConfigureMapper<ITokenMapper, TokenMapper>();
        base.ConfigureMapper<IUserRoleMapper, UserRoleMapper>();
    }

    /// <inheritdoc/>
    protected override void ConfigureRepositories()
    {
        base.ConfigureRepository<IRepository<UserManager.Model.Entity.User.Role>, RoleRepository>();
        base.ConfigureRepository<IRepository<UserManager.Model.Entity.User.AppUser>, AppUserRepository>();
    }

    /// <inheritdoc/>
    protected override void ConfigureDataStores()
    {
        base.ConfigureDataStore<IRoleDataStore, RoleDataStore>();
        base.ConfigureDataStore<IUserDataStore, UserDataStore>();
    }

    /// <inheritdoc/>
    protected override void ConfigureBusinessServices()
    {
        base.ConfigureBusinessService<IRoleService, RoleService>();
        base.ConfigureBusinessService<IAppUserService, AppUserService>();
        base.ConfigureBusinessService<ITokenService, TokenService>();
    }

    /// <inheritdoc/>
    protected override void ConfigureServices()
    {
        base.ConfigureSQLDatabase<UserManagerDataContext>();
    }

    /// <inheritdoc/>
    protected override void ConfigureWebApplication() { }

    /// <summary>
    /// Creates default roles and users for the application.
    /// </summary>
    /// <param name="serviceScope"></param>
    /// <returns></returns>
    protected override async Task SeedData(IServiceScope serviceScope)
    {
        DataSeedSettings? dataSeedSettings = serviceScope.ServiceProvider.GetService<DataSeedSettings>();

        if ((dataSeedSettings?.Users.IsNullOrEmpty()).GetValueOrDefault())
        {
            return;
        }

        UserManagerDataContext dataContext = serviceScope.ServiceProvider.GetRequiredService<UserManagerDataContext>();

        foreach (string roleName in dataSeedSettings!.Users.SelectMany(user => user.Roles)?.Distinct() ?? Array.Empty<string>())
        {
            await CreateRoleIfNotExists(serviceScope, roleName);
        }

        _ = await dataContext.SaveChangesAsync();

        foreach (UserSetting user in dataSeedSettings!.Users.DistinctBy(user => user.Username))
        {
            await CreateUserIfNotExists(serviceScope, user);
        }

        _ = await dataContext.SaveChangesAsync();

        static async Task CreateRoleIfNotExists(IServiceScope serviceScope, string roleName)
        {
            IRoleService roleService = serviceScope.ServiceProvider.GetRequiredService<IRoleService>();
            try
            {
                _ = await roleService.Get(roleName);
            }
            catch (DataNotFoundException)
            {
                _ = await roleService.Create(new CreateRoleInputModel() { Name = roleName });
            }
        }

        static async Task CreateUserIfNotExists(IServiceScope serviceScope, UserSetting userSetting)
        {
            IAppUserService appUserService = serviceScope.ServiceProvider.GetRequiredService<IAppUserService>();
            try
            {
                _ = await appUserService.Get(userSetting.Username);
            }
            catch (DataNotFoundException)
            {
                _ = await appUserService.Create(new CreateUserInputModel() { Username = userSetting.Username, Password = userSetting.Password, Roles = userSetting.Roles });
            }
        }
    }
}