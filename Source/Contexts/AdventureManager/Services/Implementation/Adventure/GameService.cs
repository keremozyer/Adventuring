using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.List;
using Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;
using Adventuring.Contexts.AdventureManager.Model.Domain.GameAggregate;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;

namespace Adventuring.Contexts.AdventureManager.Services.Implementation.Adventure;

/// <inheritdoc/>
public class GameService : IGameService
{
    private readonly IAdventureTreeService AdventureTreeService;
    private readonly IActiveUser ActiveUser;
    private readonly IGameDataStore GameDataStore;
    private readonly IGameMapper GameMapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="adventureTreeService"></param>
    /// <param name="activeUser"></param>
    /// <param name="gameDataStore"></param>
    /// <param name="gameMapper"></param>
    public GameService(IAdventureTreeService adventureTreeService, IActiveUser activeUser, IGameDataStore gameDataStore, IGameMapper gameMapper)
    {
        this.AdventureTreeService = adventureTreeService;
        this.ActiveUser = activeUser;
        this.GameDataStore = gameDataStore;
        this.GameMapper = gameMapper;
    }

    /// <inheritdoc/>
    public async Task<StartGameOutputModel> StartGame(StartGameInputModel input)
    {
        GetAdventureOutputModel adventure = await this.AdventureTreeService.Get(input.AdventureID);

        Game game = new(this.ActiveUser?.Username!, adventure.ID, input.FirstAnswer);

        AdventureNode? nextNode = input.FirstAnswer ? adventure.StartingNode.PositiveAnswerNode : adventure.StartingNode.NegativeAnswerNode;

        if (nextNode?.PositiveAnswerNode is null) // Game may immediately end after the first question.
        {
            game.EndGame();
        }

        game.ID = await this.GameDataStore.Create(game);

        return this.GameMapper.Map(game, nextNode);
    }

    /// <inheritdoc/>
    public async Task<ListGamesOutputModel> ListGamesOfActiveUser()
    {
        IReadOnlyCollection<Game>? games = await this.GameDataStore.ListGamesOfUser(this.ActiveUser?.Username!);

        return this.GameMapper.Map(games, await GetAdventureIDNameMap(this.AdventureTreeService, games?.Select(game => game.AdventureID)));

        static async Task<Dictionary<string, string>?> GetAdventureIDNameMap(IAdventureTreeService adventureTreeService, IEnumerable< string>? adventureIDs) // Game model only holds ID of the Adventure it's occurring in so we also ask the Adventure aggregate it's name and pass this mapping to output mapper.
        {
            Dictionary<string, string> map = new();
            foreach (string adventureID in adventureIDs?.Distinct() ?? Array.Empty<string>())
            {
                map.Add(adventureID, (await adventureTreeService.Get(adventureID))?.AdventureName ?? throw new DataNotFoundException(nameof(GetAdventureResponseModel), adventureID));
            }

            return map.Count == 0 ? null : map;
        }
    }

    private async Task<(Game Game, GetAdventureOutputModel Adventure)> GetGameData(string ID)
    {
        Game game = await this.GameDataStore.Get(ID) ?? throw new DataNotFoundException(nameof(Game), ID);
        GetAdventureOutputModel adventure = await this.AdventureTreeService.Get(game.AdventureID);

        return (game, adventure);
    }

    /// <inheritdoc/>
    public async Task<GetGameOutputModel> Get(string ID)
    {
        (Game game, GetAdventureOutputModel adventure) = await GetGameData(ID);

        (IReadOnlyCollection<PreviousNode> previousAnswers, string? currentMessage) = GetGameState(game, adventure);

        return this.GameMapper.Map(game, adventure, previousAnswers, currentMessage);

        static (IReadOnlyCollection<PreviousNode> previousAnswers, string? currentMessage) GetGameState(Game game, GetAdventureOutputModel adventure) // Game model only stores given answers and not node messages itself so we traverse the game tree to get previously choosen nodes' messages and the next question that should be answered.
        {
            List<PreviousNode> previousAnswers = new();
            int order = 1;
            AdventureNode currentNode = adventure.StartingNode;
            foreach (NodeAnswer answer in game.GetAnswers()) // We trust the underlying answer collection to be always in correct order so we don't make any orderings.
            {
                previousAnswers.Add(new PreviousNode(currentNode.NodeMessage, answer.ChoosenPath, order));

                currentNode = answer.ChoosenPath == true ? currentNode.PositiveAnswerNode! : currentNode.NegativeAnswerNode!;
                order++;
            }

            return (previousAnswers, DetermineLatestMessage(currentNode, previousAnswers, order));

            static string? DetermineLatestMessage(AdventureNode currentNode, List<PreviousNode> previousAnswers, int order)
            {
                string? currentMessage = null;
                if (currentNode!.PositiveAnswerNode is null) // If there are no more nodes to answer we assign the latest node to the previous answers collection without a path choosen data. 
                {
                    previousAnswers.Add(new PreviousNode(currentNode.NodeMessage, null, order));
                }
                else // If there are more nodes to answer we assign the current node's message as the next question to be answered.
                {
                    currentMessage = currentNode.NodeMessage;
                }

                return currentMessage;
            }
        }
    }

    /// <inheritdoc/>
    public async Task<AdvanceGameOutputModel> AdvanceGame(AdvanceGameInputModel input)
    {
        (Game game, GetAdventureOutputModel adventure) = await GetGameData(input.GameID);
        
        if (game.IsGameOver)
        {
            throw new BusinessException(ErrorCodes.GameIsAlreadyOver);
        }

        game.AdvanceGame(input.ChoosenPath);

        (string message, bool isEndNode) = await this.AdventureTreeService.GetTargetNodeMessage(game.AdventureID, game.GetAnswers().Select(answer => answer.ChoosenPath));

        if (isEndNode)
        {
            game.EndGame();
        }

        await this.GameDataStore.SaveNewGameState(game);

        return new AdvanceGameOutputModel(message, isEndNode);
    }
}