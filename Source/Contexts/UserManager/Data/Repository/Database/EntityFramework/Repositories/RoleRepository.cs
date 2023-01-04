using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Repository.Implementation.EntityFramework;
using Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Context;
using Adventuring.Contexts.UserManager.Model.Entity.User;

namespace Adventuring.Contexts.UserManager.Data.Repository.Database.EntityFramework.Repositories;

/// <summary>
/// Data operations for Role entity.
/// </summary>
public class RoleRepository : BaseSoftDeleteRepository<UserManagerDataContext, Role>
{
    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="activeUser"></param>
    public RoleRepository(UserManagerDataContext dataContext, IActiveUser activeUser) : base(dataContext, activeUser) { }
}