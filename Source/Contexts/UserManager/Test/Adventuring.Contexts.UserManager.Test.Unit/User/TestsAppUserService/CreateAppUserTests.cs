using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Architecture.Container.ActiveUser.Interface;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.AppUser.Create;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Adventuring.Contexts.UserManager.Services.Interface.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit.User.TestsAppUserService;

public class CreateAppUserTests : BaseUserTestFixture
{
    private readonly CreateUserInputModel ValidInputWithRole = new() { Username = "a", Password = "b", Roles = new string[] { "Player" } };
    private readonly CreateUserInputModel ValidInputWithoutRole = new() { Username = "a", Password = "b", Roles = null };
    private Mock<IActiveUser> ActiveUserMock;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.ActiveUserMock = new();
    }

    [Test]
    public async Task CreateAppUser_Success_RoleNotDeclared()
    {
        string newUsersID = "X";

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<AppUser>(null));
        _ = userDataStoreMock.Setup(x => x.Create(It.IsAny<AppUser>())).Returns(Task.FromResult(newUsersID));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns((string roleName) => Task.FromResult(new Role(roleName)));
        
        Mock<IRoleService> roleServiceMock = new();

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);
        CreateUserOutputModel result = await appUserService.Create(this.ValidInputWithoutRole);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.ID, Is.EqualTo(newUsersID));
    }

    [Test]
    public async Task CreateAppUser_Success_RoleDeclared()
    {
        string newUsersID = "X";

        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<AppUser>(null));
        _ = userDataStoreMock.Setup(x => x.Create(It.IsAny<AppUser>())).Returns(Task.FromResult(newUsersID));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns((string roleName) => Task.FromResult(new Role(roleName)));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);
        CreateUserOutputModel result = await appUserService.Create(this.ValidInputWithRole);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.ID, Is.EqualTo(newUsersID));
    }

    [Test]
    public void CreateAppUser_Input_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Create(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel)));
    }

    [Test]
    public void CreateAppUser_Input_Username_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        CreateUserInputModel input = this.ValidInputWithoutRole.DeepCopy();
        input.Username = null;

        Assert.That(async () => await appUserService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel.Username)));
    }

    [Test]
    public void CreateAppUser_Input_Username_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        CreateUserInputModel input = this.ValidInputWithoutRole.DeepCopy();
        input.Username = "";

        Assert.That(async () => await appUserService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel.Username)));
    }

    [Test]
    public void CreateAppUser_Input_Username_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        CreateUserInputModel input = this.ValidInputWithoutRole.DeepCopy();
        input.Username = "  ";

        Assert.That(async () => await appUserService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel.Username)));
    }

    [Test]
    public void CreateAppUser_Input_Password_Null()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        CreateUserInputModel input = this.ValidInputWithoutRole.DeepCopy();
        input.Password = null;

        Assert.That(async () => await appUserService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel.Password)));
    }

    [Test]
    public void CreateAppUser_Input_Password_Empty()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        CreateUserInputModel input = this.ValidInputWithoutRole.DeepCopy();
        input.Password = "";

        Assert.That(async () => await appUserService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel.Password)));
    }

    [Test]
    public void CreateAppUser_Input_Password_Whitespace()
    {
        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, new Mock<IRoleDataStore>().Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        CreateUserInputModel input = this.ValidInputWithoutRole.DeepCopy();
        input.Password = "  ";

        Assert.That(async () => await appUserService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateUserInputModel.Password)));
    }

    [Test]
    public void CreateAppUser_Role_NotExists()
    {
        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Role>(null));

        AppUserService appUserService = new(base.UserMapper, new Mock<IUserDataStore>().Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Create(this.ValidInputWithoutRole),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(Role)));
    }

    [Test]
    public void CreateAppUser_Username_AlreadyExists()
    {
        Mock<IUserDataStore> userDataStoreMock = new();
        _ = userDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new AppUser("a", "b", new Role[] { new("Player") })));

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns((string roleName) => Task.FromResult(new Role(roleName)));

        AppUserService appUserService = new(base.UserMapper, userDataStoreMock.Object, roleDataStoreMock.Object, base.AppUserDefaults, base.UserRoleMapper, base.AppUserSettings, this.ActiveUserMock.Object);

        Assert.That(async () => await appUserService.Create(this.ValidInputWithoutRole),
            Throws.TypeOf<BusinessException>().And.Property(nameof(BusinessException.Messages)).One.Property(nameof(BusinessExceptionMessage.Code)).EqualTo(ErrorCodes.UsernameAlreadyTaken));
    }
}