using Adventuring.Architecture.Application.Web.Core.Controllers;
using Adventuring.Contexts.UserManager.Mapper.Interface.User;
using Adventuring.Contexts.UserManager.Model.Contract.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Services.Interface.User;
using Microsoft.AspNetCore.Mvc;

namespace Adventuring.Contexts.UserManager.Web.API.Controllers.User;

/// <summary></summary>
public class AppUser : BaseController
{
    private readonly IUserMapper UserMapper;
    private readonly IAppUserService AppUserService;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="userMapper"></param>
    /// <param name="appUserService"></param>
    public AppUser(IUserMapper userMapper, IAppUserService appUserService)
    {
        this.UserMapper = userMapper;
        this.AppUserService = appUserService;
    }

    /// <summary>
    /// Creates a new user with given credentials and 'Player' role. If specified username already exists an error with code UsernameAlreadyTaken will be returned.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CreateUserResponseModel>> Create(CreateUserRequestModel request)
    {
        return new JsonResult(this.UserMapper.Map(await this.AppUserService.Create(this.UserMapper.Map(request))));
    }
}