using Adventuring.Architecture.AppException.Concern.Constant;
using Adventuring.Architecture.AppException.Model.Derived.Business;
using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Architecture.Concern.Extension;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Create;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit._Role.TestsRoleService;

public class CreateRoleTests : BaseRoleTestFixture
{
    private readonly CreateRoleInputModel ValidInput = new() { Name = "a" };

    [Test]
    public async Task CreateRole_Success()
    {
        string newRolesID = "x";

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Role>(null));
        _ = roleDataStoreMock.Setup(x => x.Create(It.IsAny<Role>())).Returns(Task.FromResult(newRolesID));

        CreateRoleOutputModel result = await new RoleService(roleDataStoreMock.Object, base.RoleMapper).Create(this.ValidInput);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.ID, Is.EqualTo(newRolesID));
    }

    [Test]
    public void CreateRole_Input_Null()
    {
        RoleService roleService = new(new Mock<IRoleDataStore>().Object, base.RoleMapper);

        Assert.That(async () => await roleService.Create(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateRoleInputModel)));
    }

    [Test]
    public void CreateRole_Input_Name_Null()
    {
        RoleService roleService = new(new Mock<IRoleDataStore>().Object, base.RoleMapper);

        CreateRoleInputModel input = this.ValidInput.DeepCopy();
        input.Name = null;

        Assert.That(async () => await roleService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateRoleInputModel.Name)));
    }

    [Test]
    public void CreateRole_Input_Name_Empty()
    {
        RoleService roleService = new(new Mock<IRoleDataStore>().Object, base.RoleMapper);

        CreateRoleInputModel input = this.ValidInput.DeepCopy();
        input.Name = "";

        Assert.That(async () => await roleService.Create(input),
    Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateRoleInputModel.Name)));
    }

    [Test]
    public void CreateRole_Input_Name_Whitespace()
    {
        RoleService roleService = new(new Mock<IRoleDataStore>().Object, base.RoleMapper);

        CreateRoleInputModel input = this.ValidInput.DeepCopy();
        input.Name = " ";

        Assert.That(async () => await roleService.Create(input),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(CreateRoleInputModel.Name)));
    }

    [Test]
    public void CreateRole_Role_Exists()
    {
        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(new Role("a")));

        RoleService roleService = new(roleDataStoreMock.Object, base.RoleMapper);

        Assert.That(async () => await roleService.Create(this.ValidInput),
            Throws.TypeOf<BusinessException>().And.Property(nameof(BusinessException.Messages)).One.Property(nameof(BusinessExceptionMessage.Code)).EqualTo(ErrorCodes.RoleAlreadyExists));
    }
}