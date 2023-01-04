using Adventuring.Architecture.Concern.Constant.Auth;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Microsoft.AspNetCore.Http;

namespace Adventuring.Architecture.Container.ActiveUser.Implementation.FromHttpContext;

/// <summary>
/// Gets logged in user from the HttpContext.
/// </summary>
public class ActiveHttpUser : IActiveUser
{
    private readonly HttpContext HttpContext;

    private string? IDValue;
    /// <inheritdoc/>
    public string? ID => this.IDValue ??= (this.HttpContext?.User?.Identity?.IsAuthenticated).GetValueOrDefault() ? this.HttpContext!.User.FindFirst(claim => claim.Type == UserClaimKeys.ID)!.Value : null;
    
    private IReadOnlyCollection<string>? RolesValue;
    /// <inheritdoc/>
    public IReadOnlyCollection<string>? Roles => this.RolesValue ??= (this.HttpContext?.User?.Identity?.IsAuthenticated).GetValueOrDefault() ? this.HttpContext!.User.FindAll(claim => claim.Type == UserClaimKeys.Role).Select(claim => claim.Value).ToArray() : null;
    
    private string? UserNameValue;
    /// <inheritdoc/>
    public string? Username => this.UserNameValue ??= (this.HttpContext?.User?.Identity?.IsAuthenticated).GetValueOrDefault() ? this.HttpContext!.User.Identity!.Name : null;

    /// <summary>
    /// Default dependency injecetion constructor.
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public ActiveHttpUser(IHttpContextAccessor httpContextAccessor)
    {
        this.HttpContext = httpContextAccessor.HttpContext;
    }

    /// <inheritdoc/>
    public bool HasRole(params string[] roles)
    {
        return this.Roles.IntersectSafe(roles)!.HasElements();
    }
}