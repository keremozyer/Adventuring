using Adventuring.Architecture.AppException.Model.Derived.DataNotFound;
using Adventuring.Contexts.UserManager.Data.Interface.User;
using Adventuring.Contexts.UserManager.Model.DataTransferObject.User.Role.Get;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;
using Adventuring.Contexts.UserManager.Services.Implementation.User;
using Moq;

namespace Adventuring.Contexts.UserManager.Test.Unit._Role.TestsRoleService;

public class GetRoleTests : BaseRoleTestFixture
{
    [Test]
    public async Task GetRole_Success()
    {
        Role role = new("id", "name");

        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult(role));

        GetRoleOutputModel result = await new RoleService(roleDataStoreMock.Object, base.RoleMapper).Get("a");

        Assert.That(result, Is.Not.Null);
        Assert.That(result.ID, Is.EqualTo(role.ID));
        Assert.That(result.Name, Is.EqualTo(role.Name));
    }

    [Test]
    public void GetRole_Role_NotExists()
    {
        Mock<IRoleDataStore> roleDataStoreMock = new();
        _ = roleDataStoreMock.Setup(x => x.Get(It.IsAny<string>())).Returns(Task.FromResult<Role>(null));

        RoleService roleService = new(roleDataStoreMock.Object, base.RoleMapper);

        Assert.That(async () => await roleService.Get("a"),
            Throws.TypeOf<DataNotFoundException>().And.Property(nameof(DataNotFoundException.Messages)).One.Property(nameof(DataNotFoundExceptionMessage.SearchedEntity)).EqualTo(nameof(Role)));
    }
}