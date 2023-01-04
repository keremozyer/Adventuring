using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Grant;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit.User.TestsAppUserService;

public class GrantRoleTests : BaseUserTestFixture
{
    private Mock<IActiveUser> AdminUserMock;
    private readonly GrantRoleInputModel ValidInput = new() { Username = "username", Role = "Designer" };

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.AdminUserMock = new();
        _ = this.AdminUserMock.Setup(x => x.HasRole(It.IsAny<string>())).Returns(true);
    }

    [Test]
    public async Task GrantRole_Success()
    {
        AppUser appUser = new("id", "username", "password", "salt", new List<Role> { new("Player") });

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(appUser));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns((string roleName) => Task.FromResult<Role>(new Role(roleName)));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);
        GrantRoleOutputModel result = await appUserService.GrantRole(this.ValidInput);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task GrantRole_Success_ExistingRole()
    {
        AppUser appUser = new("id", "username", "password", "salt", new List<Role> { new(this.ValidInput.Role) });

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(appUser));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);
        GrantRoleOutputModel result = await appUserService.GrantRole(this.ValidInput);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void GrantRole_Input_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        Assert.That(async () => await appUserService.GrantRole(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel)));
    }

    [Test]
    public void GrantRole_Input_Username_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Username = null;

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel.Username)));
    }

    [Test]
    public void GrantRole_Input_Username_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Username = "";

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel.Username)));
    }

    [Test]
    public void GrantRole_Input_Username_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Username = " ";

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel.Username)));
    }

    [Test]
    public void GrantRole_Input_Role_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = null;

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel.Role)));
    }

    [Test]
    public void GrantRole_Input_Role_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = "";

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel.Role)));
    }

    [Test]
    public void GrantRole_Input_Role_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = " ";

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(GrantRoleInputModel.Role)));
    }

    [Test]
    public void GrantRole_UngrantableRole()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        GrantRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = base.AppUserSettings.UngrantableRoles.First();

        Assert.That(async () => await appUserService.GrantRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(BaseAppExceptionMessage.Code)).EqualTo(ErrorCodes.RoleCannotBeGranted));
    }

    [Test]
    public void GrantRole_InvalidRole()
    {
        Mock<IActiveUser> activeUserMock = new();
        _ = activeUserMock.Setup(x => x.HasRole(It.IsAny<string>())).Returns(false);

        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, activeUserMock.Object);

        Assert.That(async () => await appUserService.GrantRole(this.ValidInput), Throws.TypeOf<UnauthorizedOperationException>());
    }

    [Test]
    public void GrantRole_User_NotExists()
    {
        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<AppUser>(null));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        Assert.That(async () => await appUserService.GrantRole(this.ValidInput),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(AppUser)));
    }

    [Test]
    public void GrantRole_Role_NotExists()
    {
        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new AppUser("a", "b", "c", "d", new Role[] { new("e") })));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Role>(null));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        Assert.That(async () => await appUserService.GrantRole(this.ValidInput),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(Role)));
    }
}