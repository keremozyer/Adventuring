using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.Concern.Constant.Auth;
using Adventuring.Architecture.Concern.Option.Auth;
using Adventuring.Contexts.UserManager.Concern.Helper;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Services.Interface.Token;
using Adventuring.Contexts.UserManager.Services.Interface.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Adventuring.Contexts.UserManager.Services.Implementation.Token;

/// <inheritdoc/>
public class TokenService : ITokenService
{
    private readonly IAppUserService AppUserService;
    private readonly TokenSettings TokenSettings;

    /// <summary>
    /// Default dependency injection constructor.
    /// </summary>
    /// <param name="appUserService"></param>
    /// <param name="tokenSettings"></param>
    public TokenService(IAppUserService appUserService, TokenSettings tokenSettings)
    {
        this.AppUserService = appUserService;
        this.TokenSettings = tokenSettings;
    }

    /// <inheritdoc/>
    public async Task<CreateTokenOutputModel> Create(CreateTokenInputModel input)
    {
        GetUserOutputModel user = await this.AppUserService.Get(input.Username);

        return ValidateUser(user, input) ? CreateToken(user, this.TokenSettings) : throw new BusinessException(ErrorCodes.InvalidPassword);

        static bool ValidateUser(GetUserOutputModel user, CreateTokenInputModel input)
        {
            return PasswordHelper.HashPassword(input.Password, Convert.FromBase64String(user.Salt)).HashedText == user.Password;
        }

        static CreateTokenOutputModel CreateToken(GetUserOutputModel user, TokenSettings tokenSettings)
        {
            DateTime currentTime = DateTime.UtcNow;

            SecurityTokenDescriptor descriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Name, user.Username),
                    new(UserClaimKeys.ID, user.ID.ToString())
                }),
                NotBefore = currentTime,
                Expires = currentTime.AddSeconds(tokenSettings.ExpiresInSeconds),
                SigningCredentials = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecurityKey!)), SecurityAlgorithms.HmacSha256)
            };

            foreach (string role in user.Roles)
            {
                descriptor.Subject.AddClaim(new Claim(UserClaimKeys.Role, role));
            }

            JwtSecurityTokenHandler handler = new();

            return new CreateTokenOutputModel(handler.WriteToken(handler.CreateToken(descriptor)), descriptor.Expires.Value, descriptor.Expires.Value.Subtract(currentTime).TotalSeconds);
        }
    }
}