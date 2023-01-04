using Adventuring.Architecture.Application.Web.Core.Controllers;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Services.Interface.User;
using Microsoft.AspNetCore.Mvc;

namespace Adventuring.Contexts.UserManager.Web.API.Controllers.User;

/// <summary></summary>
public class AppUserRoleController : BaseController
{
    private readonly IUserRoleMapper UserRoleMapper;
    private readonly IAppUserService AppUserService;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="userRoleMapper"></param>
    /// <param name="appUserService"></param>
    public AppUserRoleController(IUserRoleMapper userRoleMapper, IAppUserService appUserService)
    {
        this.UserRoleMapper = userRoleMapper;
        this.AppUserService = appUserService;
    }

    /// <summary>
    /// Grants specified role to the specified user.
    /// This service works in an idempotent manner.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<GrantRoleResponseModel>> Grant(GrantRoleRequestModel request)
    {
        return new JsonResult(this.UserRoleMapper.Map(await this.AppUserService.GrantRole(this.UserRoleMapper.Map(request))));
    }

    /// <summary>
    /// Removes specified role from the specified user.
    /// This service works in an idempotent manner.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<ActionResult<RemoveRoleResponseModel>> Remove(RemoveRoleRequestModel request)
    {
        return new JsonResult(this.UserRoleMapper.Map(await this.AppUserService.RemoveRole(this.UserRoleMapper.Map(request))));
    }
}