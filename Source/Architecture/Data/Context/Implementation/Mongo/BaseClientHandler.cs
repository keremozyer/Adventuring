using Adventuring.Architecture.Model.Entity.Implementation.Document;
using Adventuring.Contexts.AdventureManager.Concern.Option;
using MongoDB.Driver;

namespace Adventuring.Architecture.Data.Context.Implementation.Mongo;

/// <summary>
/// Base class to configure MongoDB clients.
/// </summary>
public abstract class BaseClientHandler
{
    private readonly MongoClient Client;
    /// <summary></summary>
    protected readonly IMongoDatabase Database;
    internal readonly bool DoesSupportTransactions;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mongoSettings"></param>
    public BaseClientHandler(MongoSettings mongoSettings)
    {
        this.Client = new(mongoSettings.ConnectionString);
        this.Database = this.Client.GetDatabase(mongoSettings.DatabaseName);
        this.DoesSupportTransactions = mongoSettings.DoesSupportTransactions!.Value;
    }

    /// <summary>
    /// Starts a new session and returns it without storing the session in the instance itself.
    /// </summary>
    /// <returns></returns>
    public IClientSessionHandle CreateSession()
    {
        return this.Client.StartSession();
    }

    /// <summary>
    /// Returns MongoDB collection for the specified <typeparamref name="EntityType"/>.
    /// </summary>
    /// <typeparam name="EntityType"></typeparam>
    /// <returns></returns>
    public IMongoCollection<EntityType> GetCollection<EntityType>() where EntityType : BaseDocument
    {
        return this.Database.GetCollection<EntityType>(typeof(EntityType).Name);
    }
}