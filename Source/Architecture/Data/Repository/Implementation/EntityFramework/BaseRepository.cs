using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Architecture.Data.Context.Implementation.EntityFramework;
using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Architecture.Model.Entity.Implementation.Record;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Adventuring.Architecture.Data.Repository.Implementation.EntityFramework;

/// <summary>
/// A generic implementation of basic operations for Entity Framework. Entity repositories should not inherit from this class and choose a base class among from <see cref="BaseHardDeleteRepository{DataContextType, EntityType}"/> or <see cref="BaseSoftDeleteRepository{DataContextType, EntityType}"/>.
/// </summary>
/// <typeparam name="DataContextType"></typeparam>
/// <typeparam name="EntityType"></typeparam>
public abstract class BaseRepository<DataContextType, EntityType> : IRepository<EntityType> where EntityType : BaseRecord where DataContextType : BaseDataContext
{
    /// <summary>
    /// Data context this particular repository operates on.
    /// </summary>
    protected DataContextType DataContext { get; }
    /// <summary>
    /// Data set this particular repository operates on.
    /// </summary>
    protected DbSet<EntityType> DbSet { get; }

    private readonly IActiveUser ActiveUser;

    /// <summary>
    /// Default dependency injection constructor
    /// </summary>
    /// <param name="dataContext"></param>
    /// <param name="activeUser"></param>
    public BaseRepository(DataContextType dataContext, IActiveUser activeUser)
    {
        this.DataContext = dataContext;
        this.DbSet = this.DataContext.Set<EntityType>();
        this.ActiveUser = activeUser;
    }

    private IQueryable<EntityType> ApplyFilter(Expression<Func<EntityType, bool>>? filter)
    {
        return filter is null ? this.DbSet : this.DbSet.Where(filter);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("GeneratedRegex", "SYSLIB1045:Convert to 'GeneratedRegexAttribute'.", Justification = "<Pending>")]
    private static IQueryable<EntityType> ApplyIncludes(IQueryable<EntityType> query, IEnumerable<Expression<Func<EntityType, object>>>? includes)
    {
        foreach (Expression<Func<EntityType, object>> include in includes ?? Array.Empty<Expression<Func<EntityType, object>>>())
        {
            if (include.Body.NodeType == ExpressionType.MemberAccess)
            {
                query = query.Include(include);
            }
            else
            {
                // Modifies include expression to simulate joins using .Select() and .SelectMany() feature of querying directly DbSet
                string path = Regex.Replace(include.ToString(), @"\(.?=>.?\.", ".").Replace(".SelectMany.", ".").Replace(".Select.", ".").Replace("(", String.Empty).Replace(")", String.Empty);
                query = query.Include(Regex.Replace(path, @".?=>.?\.", String.Empty));
            }
        }

        return query;
    }

    private IQueryable<EntityType> ApplyExpressions(Expression<Func<EntityType, bool>>? filter, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy, params Expression<Func<EntityType, object>>[] includes)
    {
        IQueryable<EntityType> query = ApplyIncludes(ApplyFilter(filter), includes);
        if (orderBy is not null)
        {
            query = (IQueryable<EntityType>)orderBy(query);
        }

        return query;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0046:Convert to conditional expression", Justification = "Causes triple nested ternary operator.")]
    private static IQueryable<EntityType> ApplyPaging(IQueryable<EntityType> query, int pageIndex, int pageSize)
    {
        if (pageIndex <= 0)
        {
            throw new ArgumentException($"{nameof(pageIndex)} Can't Be Less Than 1");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentException($"{nameof(pageSize)} Can't Be Less Than 1");
        }

        return query.Skip((int)((pageIndex - 1) * pageSize)).Take(pageSize);
    }

    /// <summary>
    /// Executes an SQL INSERT query.
    /// </summary>
    /// <param name="entity">Element to be inserted.</param>
    /// <returns></returns>
    public Task<string> Create(EntityType entity)
    {
        entity.CreatedBy = this.ActiveUser.ID;

        _ = this.DbSet.Add(entity);

        return Task.FromResult(entity.ID);
    }

    /// <summary>
    /// Executes an SQL Update query.
    /// </summary>
    /// <param name="entity">Element to be updated.</param>
    /// <returns></returns>
    public Task Update(EntityType entity)
    {
        EntityEntry entry = this.DataContext.Entry(entity);

        if (entry.State == EntityState.Added)
        {
            return Task.CompletedTask; // If entity is newly added calling update should not set anything.
        }

        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = this.ActiveUser.ID;

        if (entry.State == EntityState.Detached)
        {
            _ = this.DbSet.Attach(entity);
        }

        entry.State = EntityState.Modified;

        return Task.CompletedTask;
    }

    /// <summary>
    /// Executes an SQL SELECT query and returns first element that matches the criteria.
    /// </summary>
    /// <param name="filter">WHERE clause expressions.</param>
    /// <param name="orderBy">ORDER BY expression.</param>
    /// <param name="includes">JOIN expressions.</param>
    /// <returns>Single concrete element.</returns>
    public async Task<EntityType?> Get(Expression<Func<EntityType, bool>>? filter = null, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, params Expression<Func<EntityType, object>>[] includes)
    {
        return await ApplyExpressions(filter, orderBy, includes).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Executes an SQL SELECT query and returns all matching rows
    /// </summary>
    /// <param name="filter">WHERE clause expressions.</param>
    /// <param name="orderBy">ORDER BY expression.</param>
    /// <param name="pageIndex">If provided together with <paramref name="pageSize"/>, '(<paramref name="pageIndex"/> - 1) * <paramref name="pageSize"/>' elements will be skipped. If provided must be greater than 0.</param>
    /// <param name="pageSize">If provided only this much elements will be returned. If provided must be greater than 0.</param>
    /// <param name="includes">JOIN expressions.</param>
    /// <returns>Concrete list of entities.</returns>
    public async Task<List<EntityType>?> List(Expression<Func<EntityType, bool>>? filter = null, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, int? pageIndex = null, int? pageSize = null, params Expression<Func<EntityType, object>>[] includes)
    {
        ValidateQueryParameters(orderBy, pageIndex, pageSize);

        IQueryable<EntityType> entities = ApplyExpressions(filter, orderBy, includes);
        if (pageIndex is not null && pageSize is not null)
        {
            entities = ApplyPaging(entities, pageIndex.Value, pageSize.Value);
        }

        return await entities.ToListAsync();

        static void ValidateQueryParameters(Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, int? pageIndex = null, int? pageSize = null)
        {
            if (pageIndex is not null && pageSize is null)
            {
                throw new ArgumentException($"{nameof(pageSize)} Can't Be Null If {nameof(pageIndex)} Is Not Null");
            }

            if (pageIndex is null && pageSize is not null)
            {
                throw new ArgumentException($"{nameof(pageIndex)} Can't Be Null If {nameof(pageSize)} Is Not Null");
            }

            if (pageIndex is not null && pageSize is not null && orderBy is null)
            {
                throw new ArgumentException($"Paging Can't Be Done If Ordering Expression Is Not Stated.");
            }
        }
    }

    /// <summary>
    /// Executes an SQL COUNT query and returns the result.
    /// </summary>
    /// <param name="filter">WHERE clause expressions.</param>
    /// <returns></returns>
    public async Task<long> Count(Expression<Func<EntityType, bool>>? filter = null)
    {
        return await ApplyExpressions(filter, null).LongCountAsync();
    }

    /// <summary>
    /// Changes behaviour depending on the child implementation.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public abstract Task Delete(EntityType entity);
}