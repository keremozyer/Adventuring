using Adventuring.Architecture.Model.Entity.Interface;
using Adventuring.Architecture.Model.Interface.ServiceTypes;
using System.Linq.Expressions;

namespace Adventuring.Architecture.Data.Repository.Interface;

/// <summary>
/// All persistent data repositories must implement this interface.
/// </summary>
/// <typeparam name="EntityType"></typeparam>
public interface IRepository<EntityType> : IRepositoryService where EntityType : IEntity
{
    /// <summary>
    /// Saves the entity to the persistent data storage.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<string> Create(EntityType entity);
    /// <summary>
    /// Updates the entity in the persistent data storage.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task Update(EntityType entity);
    /// <summary>
    /// Deletes the entity from the persistent data storage.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task Delete(EntityType entity);
    /// <summary>
    /// Returns all matching entities in the persistent data storage.
    /// </summary>
    /// <param name="filter">Filter to apply on entities.</param>
    /// <param name="orderBy">If provided, entities will be returned in order of this expression.</param>
    /// <param name="pageIndex">If provided together with <paramref name="pageSize"/> parameter will run paging logic and return the results in the given page.</param>
    /// <param name="pageSize">If provided together with <paramref name="pageIndex"/> parameter will run paging logic and return the results in the given page.</param>
    /// <param name="includes">Child entities to include.</param>
    /// <returns></returns>
    public Task<List<EntityType>?> List(Expression<Func<EntityType, bool>>? filter = null, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, int? pageIndex = null, int? pageSize = null, params Expression<Func<EntityType, object>>[] includes);
    /// <summary>
    /// Returns the first entity that matches the criteria.
    /// </summary>
    /// <param name="filter">Filter to apply on entities.</param>
    /// <param name="orderBy">If provided, entities will be returned in order of this expression.</param>
    /// <param name="includes">Child entities to include.</param>
    /// <returns></returns>
    public Task<EntityType?> Get(Expression<Func<EntityType, bool>>? filter = null, Func<IQueryable<EntityType>, IOrderedQueryable>? orderBy = null, params Expression<Func<EntityType, object>>[] includes);
    /// <summary>
    /// Returns how many entities do match the criteria.
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task<long> Count(Expression<Func<EntityType, bool>>? filter = null);
}