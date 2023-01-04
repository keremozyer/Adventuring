using Adventuring.Architecture.AppException.Manager.Interface;
using Adventuring.Architecture.AppException.Model.Parsed;
using Adventuring.Architecture.Concern.Attribute.TransactionHandling;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Data.Context.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Adventuring.Architecture.Application.Web.Concern.Filter.ActionFilters;

/// <summary>
/// Action filter to automatically commit/rollback transactions and handle transaction errors.
/// </summary>
public class TransactionHandlerAttribute : ActionFilterAttribute
{
    /// <inheritdoc/>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (!ShouldCommitTransaction(context))
        {
            return;
        }

        IDataContext dataContext = context.HttpContext.RequestServices.GetService<IDataContext>();
        if (dataContext is null)
        {
            return;
        }

        CommitTransaction(context, dataContext);
    }

    private static bool ShouldCommitTransaction(ActionExecutedContext context)
    {
        return context.HttpContext.Response.IsSuccessful() && (context.HttpContext.Request.Method != HttpMethods.Get || HasTransactionalOperationAttribute(context));
    }

    private static bool HasTransactionalOperationAttribute(ActionExecutedContext context)
    {
        // Check for controller level attribute.
        bool hasTransactionalOperationAttribute = context.Controller.GetType().GetCustomAttribute<TransactionalOperationAttribute>(inherit: true) is not null;

        if (hasTransactionalOperationAttribute == false && context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
        {
            // If controller level attribute is not found check for action level attribute.
            hasTransactionalOperationAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<TransactionalOperationAttribute>(inherit: true) is not null;
        }

        return hasTransactionalOperationAttribute;
    }

    private static void CommitTransaction(ActionExecutedContext actionExecutedContext, IDataContext dataContext)
    {
        try
        {
            dataContext.Save().Wait();
        }
        catch (Exception exception)
        {
            HandleException(exception, actionExecutedContext);
        }
    }

    private static void HandleException(Exception exception, ActionExecutedContext actionExecutedContext)
    {
        IExceptionParser exceptionParser = actionExecutedContext.HttpContext.RequestServices.GetService<IExceptionParser>();
        ILogger<TransactionHandlerAttribute> logger = actionExecutedContext.HttpContext.RequestServices.GetService<ILogger<TransactionHandlerAttribute>>();

        ParsedException parsedException = exceptionParser.Parse(exception);
        if (parsedException.LogID is not null)
        {
            logger.LogError(parsedException.SerializeAsJson());
        }

        actionExecutedContext.Result = new JsonResult(new { parsedException.LogID, parsedException.Messages })
        {
            StatusCode = (int)parsedException.StatusCode
        };
    }
}