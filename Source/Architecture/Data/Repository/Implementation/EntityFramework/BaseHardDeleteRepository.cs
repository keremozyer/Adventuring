using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Context.Implementation.EntityFramework;
using Adventuring.Architecture.Model.Entity.Implementation.Record;

namespace Adventuring.Architecture.Data.Repository.Implementation.EntityFramework;

/// <summary>
/// All EntityFramework repositories for hard deleted entities must be derived from this class.
/// </summary>
/// <typeparam name="DataContextType"></typeparam>
/// <typeparam name="EntityType"></typeparam>
public abstract class BaseHardDeleteRepository<DataContextType, EntityType> : BaseRepository<DataContextType, EntityType> where EntityType : BaseHardDeletedRecord where DataContextType : BaseDataContext
{
    /// <summary>
    /// Default dependency injection constructor
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="activeUser"></param>
    protected BaseHardDeleteRepository(DataContextType dataContext, IActiveUser activeUser) : base(dataContext, activeUser) { }

    /// <summary>
    /// Deletes the entity completely.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public override Task Delete(EntityType entity)
    {
        _ = base.DbSet.Remove(entity);
        return Task.CompletedTask;
    }
}