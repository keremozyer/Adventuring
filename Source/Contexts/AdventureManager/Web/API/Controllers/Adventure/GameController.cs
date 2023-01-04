using Adventuring.Architecture.Application.Web.Core.Controllers;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.AdvanceGame;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.List;
using Adventuring.Contexts.AdventureManager.Model.Contract.Game.Start;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;
using Microsoft.AspNetCore.Mvc;

namespace Adventuring.Contexts.AdventureManager.Web.API.Controllers.Adventure;

/// <summary></summary>
public class GameController : BaseController
{
    private readonly IGameMapper GameMapper;
    private readonly IGameService GameService;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="gameMapper"></param>
    /// <param name="gameService"></param>
    public GameController(IGameMapper gameMapper, IGameService gameService)
    {
        this.GameMapper = gameMapper;
        this.GameService = gameService;
    }

    /// <summary>
    /// Starts a new game in the adventure specified by the adventure ID and advances the game with the choice specified in first answer.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<StartGameResponseModel>> StartGame(StartGameRequestModel request)
    {
        return new JsonResult(this.GameMapper.Map(await this.GameService.StartGame(this.GameMapper.Map(request))));
    }

    /// <summary>
    /// Lists all games started by the active user.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ListGamesResponseModel>> List()
    {
        return new JsonResult(this.GameMapper.Map(await this.GameService.ListGamesOfActiveUser()));
    }

    /// <summary>
    /// Returns the game specified by the ID parameter. Will return an error if the game specified by this ID does not exists.
    /// </summary>
    /// <param name="ID">ID of the game.</param>
    /// <returns></returns>
    [HttpGet("{ID}")]
    public async Task<ActionResult<GetGameResponseModel>> Get(string ID)
    {
        return new JsonResult(this.GameMapper.Map(await this.GameService.Get(ID)));
    }

    /// <summary>
    /// Advances the game to the next layer with the given choice. Will return an error if the game is already over.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<ActionResult<AdvanceGameResponseModel>> AdvanceGame(AdvanceGameRequestModel request)
    {
        return new JsonResult(this.GameMapper.Map(await this.GameService.AdvanceGame(this.GameMapper.Map(request))));
    }
}