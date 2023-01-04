using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;

namespace Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
/// <summary>
/// Persistent data layer for Game models.
/// </summary>
public interface IGameDataStore : IDataStoreService
{
    /// <summary>
    /// Saves the given game in the persistent data storage.
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public Task<string> Create(Game game);
    /// <summary>
    /// Returns all games created by the given player.
    /// </summary>
    /// <param name="playerName">Player's name.</param>
    /// <returns></returns>
    public Task<IReadOnlyCollection<Game>?> ListGamesOfUser(string playerName);
    /// <summary>
    /// Returns the game specified by this ID if it exists. Otherwise returns <see langword="null"/>
    /// </summary>
    /// <param name="ID">ID of the adventure.</param>
    /// <returns></returns>
    public Task<Game?> Get(string ID);
    /// <summary>
    /// Updates the with new state.
    /// </summary>
    /// <param name="game"></param>
    /// <returns></returns>
    public Task SaveNewGameState(Game game);
}