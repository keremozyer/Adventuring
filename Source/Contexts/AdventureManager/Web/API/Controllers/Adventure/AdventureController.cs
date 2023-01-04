using Adventuring.Architecture.Application.Web.Core.Controllers;
using Adventuring.Contexts.AdventureManager.Mapper.Interface.Adventure;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Create;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Get;
using Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.List;
using Adventuring.Contexts.AdventureManager.Services.Interface.Adventure;
using Microsoft.AspNetCore.Mvc;

namespace Adventuring.Contexts.AdventureManager.Web.API.Controllers.Adventure;

/// <summary></summary>
public class AdventureController : BaseController
{
    private readonly IAdventureMapper AdventureMapper;
    private readonly IAdventureTreeService AdventureTreeService;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="adventureMapper"></param>
    /// <param name="adventureTreeService"></param>
    public AdventureController(IAdventureMapper adventureMapper, IAdventureTreeService adventureTreeService)
    {
        this.AdventureMapper = adventureMapper;
        this.AdventureTreeService = adventureTreeService;
    }

    /// <summary>
    /// Creates a new adventure with the given name and initial question.
    /// Returns an error with code AdventureAlreadyExists if an adventure with the given name already exists.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CreateAdventureResponseModel>> Create(CreateAdventureRequestModel request)
    {
        return new JsonResult(this.AdventureMapper.Map(await this.AdventureTreeService.CreateAdventure(this.AdventureMapper.Map(request))));
    }

    /// <summary>
    /// Returns all adventures in the application.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<ListAdventuresResponseModel>> List()
    {
        return new JsonResult(this.AdventureMapper.Map(await this.AdventureTreeService.List()));
    }

    /// <summary>
    /// Returns the adventure specified by the ID. An error will be returned if the adventure does not exists.
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    [HttpGet("{ID}")]
    public async Task<ActionResult<GetAdventureResponseModel>> Get(string ID)
    {
        return new JsonResult(this.AdventureMapper.Map(await this.AdventureTreeService.Get(ID)));
    }
}