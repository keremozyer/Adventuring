using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Base;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.UnauthorizedOperation;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUserRole.Remove;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit.User.TestsAppUserService;

public class RemoveRoleTests : BaseUserTestFixture
{
    private Mock<IActiveUser> AdminUserMock;
    private readonly RemoveRoleInputModel ValidInput = new() { Username = "username", Role = "Designer" };

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.AdminUserMock = new();
        _ = this.AdminUserMock.Setup(x => x.HasRole(It.IsAny<string>())).Returns(true);
    }

    [Test]
    public async Task RemoveRole_Success()
    {
        AppUser appUser = new("id", "username", "password", "salt", new List<Role> { new("Player"), new("Designer") });

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(appUser));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns((string roleName) => Task.FromResult<Role>(new Role(roleName)));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);
        RemoveRoleOutputModel result = await appUserService.RemoveRole(this.ValidInput);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public async Task RemoveRole_Success_NonExistingRole()
    {
        AppUser appUser = new("id", "username", "password", "salt", new List<Role> { new("Player") });

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(appUser));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);
        RemoveRoleOutputModel result = await appUserService.RemoveRole(this.ValidInput);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void RemoveRole_Input_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        Assert.That(async () => await appUserService.RemoveRole(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel)));
    }

    [Test]
    public void RemoveRole_Input_Username_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Username = null;

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel.Username)));
    }

    [Test]
    public void RemoveRole_Input_Username_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Username = "";

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel.Username)));
    }

    [Test]
    public void RemoveRole_Input_Username_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Username = " ";

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel.Username)));
    }

    [Test]
    public void RemoveRole_Input_Role_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = null;

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel.Role)));
    }

    [Test]
    public void RemoveRole_Input_Role_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = "";

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel.Role)));
    }

    [Test]
    public void RemoveRole_Input_Role_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = " ";

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(RemoveRoleInputModel.Role)));
    }

    [Test]
    public void RemoveRole_UnremovableRole()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        RemoveRoleInputModel input = this.ValidInput.DeepCopy();
        input.Role = base.AppUserDefaults.DefaultRole;

        Assert.That(async () => await appUserService.RemoveRole(input),
            Throws.TypeOf<BaseAppException>().And.Property(nameof(BaseAppException.Messages)).One.Property(nameof(BaseAppExceptionMessage.Code)).EqualTo(ErrorCodes.DefaultRoleCannotBeRemoved));
    }

    [Test]
    public void RemoveRole_InvalidRole()
    {
        Mock<IActiveUser> activeUserMock = new();
        _ = activeUserMock.Setup(x => x.HasRole(It.IsAny<string>())).Returns(false);

        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, activeUserMock.Object);

        Assert.That(async () => await appUserService.RemoveRole(this.ValidInput), Throws.TypeOf<UnauthorizedOperationException>());
    }

    [Test]
    public void RemoveRole_User_NotExists()
    {
        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<AppUser>(null));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        Assert.That(async () => await appUserService.RemoveRole(this.ValidInput),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(AppUser)));
    }

    [Test]
    public void RemoveRole_Role_NotExists()
    {
        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new AppUser("a", "b", "c", "d", new Role[] { new(this.ValidInput.Role) })));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Role>(null));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.AdminUserMock.Object);

        Assert.That(async () => await appUserService.RemoveRole(this.ValidInput),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(Role)));
    }
}