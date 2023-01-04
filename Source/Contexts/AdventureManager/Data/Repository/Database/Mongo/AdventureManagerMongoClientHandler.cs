using Adventuring.Architecture.Data.Context.Implementation.Mongo;
using Adventuring.Contexts.AdventureManager.Concern.Option;
using Adventuring.Contexts.AdventureManager.Model.Entity._Game;
using Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;
using MongoDB.Driver;

namespace Adventuring.Contexts.AdventureManager.Data.Repository.Database.Mongo;

/// <summary>
/// Client configurations for the AdventureManager MongoDB database.
/// </summary>
public class AdventureManagerMongoClientHandler : BaseClientHandler
{
    internal IMongoCollection<AdventureTree> AdventureTrees { get; }
    internal IMongoCollection<Game> Games { get; }

    /// <summary>
    /// Default dependency injection constructor
    /// </summary>
    /// <param name="mongoSettings"></param>
    public AdventureManagerMongoClientHandler(MongoSettings mongoSettings) : base(mongoSettings)
    {
        this.AdventureTrees = base.Database.GetCollection<AdventureTree>(nameof(this.AdventureTrees));
        this.Games = base.Database.GetCollection<Game>(nameof(this.Games));
    }
}