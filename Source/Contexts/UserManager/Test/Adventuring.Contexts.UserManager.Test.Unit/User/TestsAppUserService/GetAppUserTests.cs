using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Get;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Adventuring.Contexts.UserManager.Services.Interface.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit.User.TestsAppUserService;

public class GetAppUserTests : BaseUserTestFixture
{
    private Mock<IActiveUser> ActiveUserMock;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.ActiveUserMock = new();
    }

    [Test]
    public async Task GetAppUser_Success()
    {
        AppUser user = new("id", "username", "password", "salt", new Role[] { new("Player") });

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(user));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);
        GetUserOutputModel result = await appUserService.Get("a");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.ID, Is.EqualTo(user.ID));
        Assert.That(result.Username, Is.EqualTo(user.Username));
    }

    [Test]
    public void GetAppUser_User_NotFound()
    {
        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<AppUser>(null));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Get("a"),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(AppUser)));
    }

    [Test]
    public void GetAppUser_Input_Username_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Get(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo("username"));
    }

    [Test]
    public void GetAppUser_Input_Username_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Get(""),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo("username"));
    }

    [Test]
    public void GetAppUser_Input_Username_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Get("  "),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo("username"));
    }
}