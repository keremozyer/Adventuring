using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Contexts.UserManager.Concern.Helper;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.Token.Create;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Services.Implementation.Token;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Adventuring.Contexts.UserManager.Services.Interface.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit.Token.TestsTokenService;

public class CreateTokenTests : BaseTokenTestFixture
{
    [Test]
    public async Task CreateToken_Success()
    {
        string password = "Pass";
        (string hashedPassword, byte[] salt) = PasswordHelper.HashPassword(password);
        string saltBase64 = Convert.ToBase64String(salt);

        Mock<IAppUserService> appUserServiceMock = new();
        _ = appUserServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new GetUserOutputModel() { ID = "a", Username = "b", Salt = saltBase64, Password = hashedPassword, Roles = new string[] { "Player" } }));
        
        CreateTokenOutputModel result = await new TokenService(appUserServiceMock.Object, base.TokenSettings).Create(new CreateTokenInputModel() { Username = "user", Password = password });

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Token, Is.Not.Null);
        Assert.That(result.Token, Is.Not.Empty);
        Assert.That(result.ExpiresInSeconds, Is.Not.Zero);
        Assert.That(result.ExpiresAt, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void CreateToken_InvalidPassword()
    {
        string password = "Pass";
        (string hashedPassword, byte[] salt) = PasswordHelper.HashPassword(password);
        string saltBase64 = Convert.ToBase64String(salt);

        Mock<IAppUserService> appUserServiceMock = new();
        _ = appUserServiceMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new GetUserOutputModel() { ID = "a", Username = "b", Salt = saltBase64, Password = hashedPassword, Roles = new string[] { "Player" } }));

        TokenService tokenService = new(appUserServiceMock.Object, base.TokenSettings);

        Assert.That(async () => await tokenService.Create(new CreateTokenInputModel() { Username = "a", Password = password + password}),
            Throws.TypeOf<BusinessException>().And.Property(nameof(BusinessException.Messages)).One.Property(nameof(BusinessExceptionMessage.Code)).EqualTo(ErrorCodes.InvalidPassword));
    }
}