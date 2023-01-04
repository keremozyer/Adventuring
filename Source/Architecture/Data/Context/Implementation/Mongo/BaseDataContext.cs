using Adventuring.Architecture.Data.Context.Interface;
using Adventuring.Architecture.Model.Entity.Implementation.Document;
using MongoDB.Driver;

namespace Adventuring.Architecture.Data.Context.Implementation.Mongo;

/// <summary>
/// DataContext class to handle MongoDB connections.
/// </summary>
public abstract class BaseDataContext : IDataContext
{
    private readonly BaseClientHandler ClientHandler;
    private readonly IClientSessionHandle? Session;
    private bool IsTransactionCommited;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="clientHandler"></param>
    public BaseDataContext(BaseClientHandler clientHandler)
    {
        this.ClientHandler = clientHandler;

        if (this.ClientHandler.DoesSupportTransactions)
        {
            this.Session = clientHandler.CreateSession();
            this.Session.StartTransaction();
        }
    }

    /// <inheritdoc/>
    public async Task Save(CancellationToken cancellationToken = default)
    {
        if (this.Session is null)
        {
            return;
        }

        await this.Session.CommitTransactionAsync(cancellationToken);
        this.IsTransactionCommited = true;
    }

    /// <summary>
    /// Returns MongoDB collection for the specified <typeparamref name="EntityType"/>.
    /// </summary>
    /// <typeparam name="EntityType"></typeparam>
    /// <returns></returns>
    public IMongoCollection<EntityType> GetCollection<EntityType>() where EntityType : BaseDocument
    {
        return this.ClientHandler.GetCollection<EntityType>();
    }

    /// <summary>
    /// Aborts the ongoing transaction if there is one.
    /// </summary>
    ~BaseDataContext()
    {
        if ((this.Session?.IsInTransaction).GetValueOrDefault() && !this.IsTransactionCommited)
        {
            this.Session!.AbortTransaction();
        }
    }
}