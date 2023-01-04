using Adventuring.Architecture.AppException.Manager.Interface;
using Adventuring.Architecture.AppException.Model.Parsed;
using Adventuring.Architecture.Concern.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Adventuring.Architecture.Application.Web.Core.Controllers;

/// <summary>
/// Provides standardization for error handling in WEB APIs.
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public abstract class BaseErrorController : ControllerBase
{
    private readonly IExceptionParser ExceptionParser;
    private readonly ILogger<BaseErrorController> Logger;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="exceptionParser"></param>
    /// <param name="logger"></param>
    public BaseErrorController(IExceptionParser exceptionParser, ILogger<BaseErrorController> logger)
    {
        this.ExceptionParser = exceptionParser;
        this.Logger = logger;
    }

    /// <summary>
    /// Whenever there is an error in the HTTP request pipeline this method will be called last and will return an appropriate result depending on the error.
    /// </summary>
    /// <returns></returns>
    [Route(nameof(Error))]
    public IActionResult Error()
    {
        IExceptionHandlerFeature handler = base.HttpContext.Features.GetRequiredFeature<IExceptionHandlerFeature>();
        ParsedException parsedException = this.ExceptionParser.Parse(handler.Error);

        if (!String.IsNullOrWhiteSpace(parsedException.LogID))
        {
            this.Logger.LogError(handler.Error, parsedException.SerializeAsJson());
        }

        return new ObjectResult(new { parsedException.LogID, parsedException.Messages })
        {
            StatusCode = (int)parsedException.StatusCode
        };
    }
}