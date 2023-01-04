using Adventuring.Architecture.Application.Web.Concern.Filter.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Adventuring.Architecture.Application.Web.Core.Controllers;

/// <summary>
/// All API controllers must be derived from this class to have common configurations.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[TransactionHandler]
public abstract class BaseController : ControllerBase
{

}