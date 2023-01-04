using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.Data.Repository.Interface;
using Adventuring.Contexts.AdventureManager.Data.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;

namespace Adventuring.Contexts.AdventureManager.Data.Implementation.Adventure;

/// <inheritdoc/>
public class GameDataStore : IGameDataStore
{
    private readonly IRepository<Model.Entity._Game.Game> GameRepository;
    private readonly IGameMapper GameMapper;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="gameRepository"></param>
    /// <param name="gameMapper"></param>
    public GameDataStore(IRepository<Model.Entity._Game.Game> gameRepository, IGameMapper gameMapper)
    {
        this.GameRepository = gameRepository;
        this.GameMapper = gameMapper;
    }

    /// <inheritdoc/>
    public async Task<string> Create(Model.Domain.GameAggregate.Game game)
    {
        Model.Entity._Game.Game gameEntity = this.GameMapper.MapGame(game);

        _ = await this.GameRepository.Create(gameEntity);

        return gameEntity.ID;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<Model.Domain.GameAggregate.Game>?> ListGamesOfUser(string playerName)
    {
        IReadOnlyCollection<Model.Entity._Game.Game>? gameEntities = await this.GameRepository.List(game => game.PlayerName == playerName);

        return this.GameMapper.Map(gameEntities);
    }

    /// <inheritdoc/>
    public async Task<Model.Domain.GameAggregate.Game?> Get(string ID)
    {
        Model.Entity._Game.Game? gameEntity = await this.GameRepository.Get(game => game.ID == ID);

        return this.GameMapper.MapGame(gameEntity);
    }

    /// <inheritdoc/>
    public async Task SaveNewGameState(Model.Domain.GameAggregate.Game game)
    {
        Model.Entity._Game.Game gameEntity = await this.GameRepository.Get(x => x.ID == game.ID) ?? throw new DataNotFoundException(nameof(Model.Entity._Game.Game), game.ID);

        gameEntity.Answers = game.GetAnswers().Select(answer => answer.ChoosenPath).ToList();
        gameEntity.IsGameOver = game.IsGameOver;

        await this.GameRepository.Update(gameEntity);
    }
}