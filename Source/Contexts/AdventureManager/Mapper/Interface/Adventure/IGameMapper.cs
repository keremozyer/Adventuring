using Adventuring.Architecture.Model.Interface.ServiceTypes;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.List;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Start;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;

namespace Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;

/// <summary>
/// Maps related to the Game model.
/// </summary>
public interface IGameMapper : IMapperService
{
    /// <summary>
    /// Maps given <see cref="Model.Domain.GameAggregate.Game"/> Domain Model to <see cref="Model.Entity._Game.Game"/> Entity.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Entity._Game.Game MapGame(Model.Domain.GameAggregate.Game model);
    /// <summary>
    /// Maps given <see cref="Model.Entity._Game.Game"/> Entity to <see cref="Model.Domain.GameAggregate.Game"/> Domain Model.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Model.Domain.GameAggregate.Game? MapGame(Model.Entity._Game.Game? model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.GameAggregate.Game"/> Domain Model to <see cref="StartGameOutputModel"/> DTO. Uses <paramref name="nextNode"/> to return the second question in the game.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="nextNode"></param>
    /// <returns></returns>
    public StartGameOutputModel Map(Model.Domain.GameAggregate.Game model, AdventureNode? nextNode);
    /// <summary>
    /// Maps given <see cref="StartGameRequestModel"/> Contract to <see cref="StartGameInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public StartGameInputModel Map(StartGameRequestModel model);
    /// <summary>
    /// Maps given <see cref="StartGameOutputModel"/> DTO to <see cref="StartGameResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public StartGameResponseModel Map(StartGameOutputModel model);
    /// <summary>
    /// Maps given <see cref="Model.Entity._Game.Game"/> Entities to <see cref="Model.Domain.GameAggregate.Game"/> Domain Models.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public IReadOnlyCollection<Model.Domain.GameAggregate.Game>? Map(IReadOnlyCollection<Model.Entity._Game.Game>? model);
    /// <summary>
    /// Maps given <see cref="Model.Domain.GameAggregate.Game"/> Domain Models to <see cref="ListGamesOutputModel"/> DTO. Uses <paramref name="adventureIDNameMap"/> to put adventure names in the response.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="adventureIDNameMap"></param>
    /// <returns></returns>
    public ListGamesOutputModel Map(IReadOnlyCollection<Model.Domain.GameAggregate.Game>? model, IDictionary<string, string>? adventureIDNameMap);
    /// <summary>
    /// Maps given <see cref="ListGamesOutputModel"/> DTO to <see cref="ListGamesResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public ListGamesResponseModel Map(ListGamesOutputModel model);
    /// <summary>
    /// Maps given <see cref="GetGameOutputModel"/> DTO to <see cref="GetGameResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public GetGameResponseModel Map(GetGameOutputModel model);
    /// <summary>
    /// Maps given <see cref="AdvanceGameRequestModel"/> Contract to <see cref="AdvanceGameInputModel"/> DTO.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public AdvanceGameInputModel Map(AdvanceGameRequestModel model);
    /// <summary>
    /// Maps given <see cref="AdvanceGameOutputModel"/> DTO to <see cref="AdvanceGameResponseModel"/> Contract.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public AdvanceGameResponseModel Map(AdvanceGameOutputModel model);
    /// <summary>
    /// Creates the <see cref="GetGameOutputModel"/> DTO from the data determined for the game.
    /// </summary>
    /// <param name="game">Game itself.</param>
    /// <param name="adventure">Adventure data that game is occurring in.</param>
    /// <param name="previousAnswers">Previously given answers in the game.</param>
    /// <param name="currentMessage">Next node's message.</param>
    /// <returns></returns>
    GetGameOutputModel Map(Model.Domain.GameAggregate.Game game, GetAdventureOutputModel adventure, IReadOnlyCollection<PreviousNode> previousAnswers, string? currentMessage);
}