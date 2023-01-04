using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Context.Implementation.EntityFramework;
using Adventuring.Architecture.Model.Entity.Implementation.Record;

namespace Adventuring.Architecture.Data.Repository.Implementation.EntityFramework;

/// <summary>
/// All EntityFramework repositories for soft deleted entities must be derived from this class.
/// </summary>
/// <typeparam name="DataContextType"></typeparam>
/// <typeparam name="EntityType"></typeparam>
public abstract class BaseSoftDeleteRepository<DataContextType, EntityType> : BaseRepository<DataContextType, EntityType> where EntityType : BaseSoftDeletedRecord where DataContextType : BaseDataContext
{
    /// <summary>
    /// Default dependency injection constructor
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="activeUser"></param>
    protected BaseSoftDeleteRepository(DataContextType dataContext, IActiveUser activeUser) : base(dataContext, activeUser) { }

    /// <summary>
    /// Sets the entity's DeletedAt property and updates it.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public override async Task Delete(EntityType entity)
    {
        entity.DeletedAt = DateTime.UtcNow;

        await base.Update(entity);
    }
}