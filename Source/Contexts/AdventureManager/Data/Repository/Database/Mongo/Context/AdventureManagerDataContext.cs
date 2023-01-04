using Adventuring.Architecture.Data.Context.Implementation.Mongo;
using Adventuring.Contexts.AdventureManager.Model.Entity._Game;
using Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;
using MongoDB.Driver;

namespace Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo.Context;

/// <summary>
/// MongoDB DataContext class to handle AdventureManager database operations.
/// </summary>
public class AdventureManagerDataContext : BaseDataContext
{
    /// <summary></summary>
    public IMongoCollection<AdventureTree> AdventureTrees { get; }
    /// <summary></summary>
    public IMongoCollection<Game> Games { get; }

    /// <summary>
    /// Default dependency injection constructor
    /// </summary>
    /// <param name="clientHandler"></param>
    public AdventureManagerDataContext(AdventureManagerMongoClientHandler clientHandler) : base(clientHandler)
    {
        this.AdventureTrees = clientHandler.AdventureTrees;
        _ = this.AdventureTrees.Indexes.CreateOneAsync(new CreateIndexModel<AdventureTree>(Builders<AdventureTree>.IndexKeys.Ascending(adventureTree => adventureTree.AdventureName)));

        this.Games = clientHandler.Games;
        _ = this.Games.Indexes.CreateOneAsync(new CreateIndexModel<Game>(Builders<Game>.IndexKeys.Ascending(adventureTree => adventureTree.PlayerName)));
    }
}