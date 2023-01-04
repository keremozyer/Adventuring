using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Context.Implementation.Mongo;
using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Architecture.Model.Entity.Implementation.Document;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Adventuring.Architecture.Data.Repository.Implementation.Mongo;

/// <summary>
/// A generic implementation of basic operations for MongoDB.
/// </summary>
/// <typeparam name="DataContextType"></typeparam>
/// <typeparam name="EntityType"></typeparam>
public abstract class BaseRepository<DataContextType, EntityType> : IRepository<EntityType> where EntityType : BaseDocument where DataContextType : BaseDataContext
{
    private readonly IActiveUser ActiveUser;
    /// <summary>
    /// Data context this particular repository operates on.
    /// </summary>
    protected readonly DataContextType DataContext;
    /// <summary>
    /// Data set this particular repository operates on.
    /// </summary>
    protected readonly IMongoCollection<EntityType> Collection;

    /// <summary>
    /// Default dependency injection constructor
    /// </summary>
    /// <param name="activeUser"></param>
    /// <param name="dataContext"></param>
    protected BaseRepository(IActiveUser activeUser, DataContextType dataContext)
    {
        this.ActiveUser = activeUser;
        this.DataContext = dataContext;
        this.Collection = this.DataContext.GetCollection<EntityType>();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "<Pending>")]
    private static IFindFluent<EntityType, EntityType> ApplyPaging(IFindFluent<EntityType, EntityType> query, int pageIndex, int pageSize)
    {
        if (pageIndex <= 0)
        {
            throw new ArgumentException($"{nameof(pageIndex)} Can't Be Less Than 1");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException($"{nameof(pageSize)} Can't Be Less Than 1");
        }

        return query.Skip((int)((pageIndex - 1) * pageSize)).Limit(pageSize);
    }

    /// <inheritdoc/>
    public async Task<long> Count(Expression<Func<EntityType, bool>>? filter = null)
    {
        filter ??= x => true;
        return await this.Collection.Find(filter).CountDocumentsAsync();
    }

    /// <inheritdoc/>
    public async Task<string> Create(EntityType entity)
    {
        entity.CreatedBy = this.ActiveUser?.ID;

        await this.Collection.InsertOneAsync(entity);

        return entity.ID;
    }

    /// <inheritdoc/>
    public async Task Delete(EntityType entity)
    {
        _ = await this.Collection.DeleteOneAsync(filter => filter.ID == entity.ID);
    }

    /// <inheritdoc/>
    public async Task<EntityType?> Get(Expression<Func<EntityType, bool>>? filter = null, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, params Expression<Func<EntityType, object>>[] includes)
    {
        filter ??= x => true;
        return await this.Collection.Find(filter).FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<List<EntityType>?> List(Expression<Func<EntityType, bool>>? filter = null, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, int? pageIndex = null, int? pageSize = null, params Expression<Func<EntityType, object>>[] includes)
    {
        filter ??= x => true;
        IFindFluent<EntityType, EntityType> query = this.Collection.Find(filter);

        if (pageIndex is not null && pageSize is not null)
        {
            query = ApplyPaging(query, pageIndex.Value, pageSize.Value);
        }

        return await query.ToListAsync();
    }

    /// <inheritdoc/>
    public async Task Update(EntityType entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = this.ActiveUser?.ID;

        _ = await this.Collection.ReplaceOneAsync(x => x.ID == entity.ID, entity);
    }
}