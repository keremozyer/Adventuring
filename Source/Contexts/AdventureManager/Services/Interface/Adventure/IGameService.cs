using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;

namespace Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;

/// <summary>
/// Business flows for adventure tree model.
/// </summary>
public interface IGameService : IBusinessService
{
    /// <summary>
    /// Stars a new game occurring in the given adventure for the active user and advances the game with the answer given in <see cref="StartGameInputModel.FirstAnswer"/>.
    /// Will throw a ValidationException if an active user not exists.
    /// Will throw a DataNotFoundException if an adventure with the ID of <see cref="StartGameInputModel.AdventureID"/>.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<StartGameOutputModel> StartGame(StartGameInputModel input);
    /// <summary>
    /// Returns all games of the active user. <see cref="ListGamesOutputModel"/> won't be <see langword="null"/> if no games found but the <see cref="ListGamesOutputModel.Games"/> will be.
    /// </summary>
    /// <returns></returns>
    public Task<ListGamesOutputModel> ListGamesOfActiveUser();
    /// <summary>
    /// Returns the game specified by the <paramref name="ID"/>.
    /// Will throw a DataNotfoundException if a game with the ID of <paramref name="ID"/> is not found.
    /// </summary>
    /// <param name="ID">ID of the game.</param>
    /// <returns></returns>
    public Task<GetGameOutputModel> Get(string ID);
    /// <summary>
    /// Advances the game specified in the <see cref="AdvanceGameInputModel.GameID"/> in the specified by the <see cref="AdvanceGameInputModel.ChoosenPath"/>.
    /// Will throw a BusinessException with GameIsAlreadyOver error code if the is already ended.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Task<AdvanceGameOutputModel> AdvanceGame(AdvanceGameInputModel input);
}