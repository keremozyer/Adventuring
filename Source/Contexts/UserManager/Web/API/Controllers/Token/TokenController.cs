using Adventuring.Architecture.Application.Web.Core.Controllers;
using Adventuring.Contexts.UserManager.Mapper.Interface.Token;
using Adventuring.Contexts.UserManager.Model.Contract.Token.Create;
using Adventuring.Contexts.UserManager.Services.Interface.Token;
using Microsoft.AspNetCore.Mvc;

namespace Adventuring.Contexts.UserManager.Web.API.Controllers.Token;

/// <summary></summary>
public class TokenController : BaseController
{
    private readonly ITokenMapper TokenMapper;
    private readonly ITokenService TokenService;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="tokenMapper"></param>
    /// <param name="tokenService"></param>
    public TokenController(ITokenMapper tokenMapper, ITokenService tokenService)
    {
        this.TokenMapper = tokenMapper;
        this.TokenService = tokenService;
    }

    /// <summary>
    /// Creates a JWT token containing given user's roles and returns token and token-related metadata.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CreateTokenResponseModel>> Create(CreateTokenRequestModel request)
    {
        return new JsonResult(this.TokenMapper.Map(await this.TokenService.Create(this.TokenMapper.Map(request))));
    }
}