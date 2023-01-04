using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.List;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Start;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;
using AutoMapper;

namespace Adventuring.Contexts.AdventureManager.Mapper.Implementation.Mappers.Adventure;

/// <inheritdoc/>
public class GameMapper : IGameMapper
{
    private readonly IMapper Mapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="mapper"></param>
    public GameMapper(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    /// <inheritdoc/>
    public Model.Entity._Game.Game MapGame(Model.Domain.GameAggregate.Game model)
    {
        return this.Mapper.Map<Model.Entity._Game.Game>(model);
    }

    /// <inheritdoc/>
    public Model.Domain.GameAggregate.Game? MapGame(Model.Entity._Game.Game? model)
    {
        return this.Mapper.Map<Model.Domain.GameAggregate.Game>(model);
    }

    /// <inheritdoc/>
    public StartGameOutputModel Map(Model.Domain.GameAggregate.Game model, AdventureNode? nextNode)
    {
        StartGameOutputModel output = this.Mapper.Map<StartGameOutputModel>(model);
        output.NextMessage = nextNode?.NodeMessage;
        output.IsEndNode = nextNode?.PositiveAnswerNode is null;

        return output;
    }

    /// <inheritdoc/>
    public StartGameInputModel Map(StartGameRequestModel model)
    {
        return this.Mapper.Map<StartGameInputModel>(model);
    }

    /// <inheritdoc/>
    public StartGameResponseModel Map(StartGameOutputModel model)
    {
        return this.Mapper.Map<StartGameResponseModel>(model);
    }

    /// <inheritdoc/>
    public IReadOnlyCollection<Model.Domain.GameAggregate.Game>? Map(IReadOnlyCollection<Model.Entity._Game.Game>? model)
    {
        return model?.Select(MapGame)?.ToList()!;
    }

    /// <inheritdoc/>
    public GameModel Map(Model.Domain.GameAggregate.Game? model)
    {
        return this.Mapper.Map<GameModel>(model);
    }

    /// <inheritdoc/>
    public ListGamesOutputModel Map(IReadOnlyCollection<Model.Domain.GameAggregate.Game>? model, IDictionary<string, string>? adventureIDNameMap)
    {
        return new ListGamesOutputModel()
        {
            Games = model?.Select(game =>
            {
                GameModel gameModel = Map(game);
                gameModel.AdventureName = adventureIDNameMap![gameModel.AdventureID];

                return gameModel;
            })?.ToList()
        };
    }

    /// <inheritdoc/>
    public ListGamesResponseModel Map(ListGamesOutputModel model)
    {
        return this.Mapper.Map<ListGamesResponseModel>(model);
    }

    /// <inheritdoc/>
    public GetGameResponseModel Map(GetGameOutputModel model)
    {
        return this.Mapper.Map<GetGameResponseModel>(model);
    }

    /// <inheritdoc/>
    public AdvanceGameInputModel Map(AdvanceGameRequestModel model)
    {
        return this.Mapper.Map<AdvanceGameInputModel>(model);
    }

    /// <inheritdoc/>
    public AdvanceGameResponseModel Map(AdvanceGameOutputModel model)
    {
        return this.Mapper.Map<AdvanceGameResponseModel>(model);
    }

    /// <inheritdoc/>
    public GetGameOutputModel Map(Model.Domain.GameAggregate.Game game, GetAdventureOutputModel adventure, IReadOnlyCollection<PreviousNode> previousAnswers, string? currentMessage)
    {
        return new()
        {
            GameData = new GameModel()
            {
                ID = game.ID,
                AdventureID = game.AdventureID,
                AdventureName = adventure.AdventureName,
                IsGameOver = game.IsGameOver,
            },
            PreviousAnswers = previousAnswers,
            CurrentMessage = currentMessage
        };
    }
}