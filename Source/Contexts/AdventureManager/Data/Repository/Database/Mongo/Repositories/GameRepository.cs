using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Repository.Implementation.Mongo;
using Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Context;
using Adventuring.Contexts.AdventureManager.Model.Entity._Game;

namespace Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Repositories;

/// <summary>
/// Data operations for AdventureTree entity.
/// </summary>
public class GameRepository : BaseRepository<AdventureManagerDataContext, Game>
{
    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="activeUser"></param>
    /// <param name="dataContext"></param>
    public GameRepository(IActiveUser activeUser, AdventureManagerDataContext dataContext) : base(activeUser, dataContext) { }
}