using Adventuring.Architecture.AppException.Manager.Interface;
using Adventuring.Architecture.Application.Web.Core.Controllers;

namespace Adventuring.Contexts.AdventureManager.Web.API.Controllers._Pipeline;

/// <inheritdoc/>
public class ErrorController : BaseErrorController
{
    /// <inheritdoc/>
    public ErrorController(IExceptionParser exceptionParser, ILogger<BaseErrorController> logger) : base(exceptionParser, logger) { }
}