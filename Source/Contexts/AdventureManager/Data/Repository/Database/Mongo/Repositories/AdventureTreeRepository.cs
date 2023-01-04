using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Repository.Implementation.Mongo;
using Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Context;
using Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;

namespace Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Repositories;

/// <summary>
/// Data operations for AdventureTree entity.
/// </summary>
public class AdventureTreeRepository : BaseRepository<AdventureManagerDataContext, AdventureTree>
{
    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="activeUser"></param>
    /// <param name="dataContext"></param>
    public AdventureTreeRepository(IActiveUser activeUser, AdventureManagerDataContext dataContext) : base(activeUser, dataContext) { }
}