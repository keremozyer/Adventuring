using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.AdventureManager.Model.Domain.AdventureAggregate;

namespace Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;

/// <summary>
/// Persistent data layer for Adventure models.
/// </summary>
public interface IAdventureDataStore : IDataStoreService
{
    /// <summary>
    /// Saves the given adventure in the persistent data storage and returns the ID of the newly created adventure.
    /// </summary>
    /// <param name="adventureTree"></param>
    /// <returns></returns>
    public Task<string> Create(AdventureTree adventureTree);
    /// <summary>
    /// Returns the adventure specified by this ID if it exists. Otherwise returns <see langword="null"/>
    /// </summary>
    /// <param name="ID">ID of the adventure.</param>
    /// <returns></returns>
    public Task<AdventureTree?> Get(string ID);
    /// <summary>
    /// Returns the adventure specified by this name if it exists. Otherwise returns <see langword="null"/>.
    /// Tries to match the adventure name as-is without doing any trims or case changes.
    /// </summary>
    /// <param name="adventureName">Name of the adventure.</param>
    /// <returns></returns>
    public Task<AdventureTree?> GetByName(string adventureName);
    /// <summary>
    /// Lists all adventures in the application.
    /// </summary>
    /// <returns></returns>
    public Task<IReadOnlyCollection<AdventureTree>?> List();
}