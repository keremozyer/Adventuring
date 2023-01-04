using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Repository.Implementation.EntityFramework;
using Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Context;
using Adventuring.Contexts.UserManager.Model.Entity.User;

namespace Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Repositories;

/// <summary>
/// Data operations for AppUser entity.
/// </summary>
public class AppUserRepository : BaseSoftDeleteRepository<UserManagerDataContext, AppUser>
{
    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="activeUser"></param>
    public AppUserRepository(UserManagerDataContext dataContext, IActiveUser activeUser) : base(dataContext, activeUser) { }
}