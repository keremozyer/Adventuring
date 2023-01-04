using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;

namespace Adventuring.Contexts.UserManager.Test.Unit._Role.Domain;

public class RoleTests : BaseRoleTestFixture
{
    [Test]
    public void Role_Create_Success()
    {
        string roleName = "a";

        Role role = new(roleName);

        Assert.That(role.Name, Is.EqualTo(roleName));
    }

    [Test]
    public void CreateRole_Input_Name_Null()
    {
        Assert.That(() => new Role(null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Role.Name)));
    }

    [Test]
    public void CreateRole_Input_Name_Empty()
    {
        Assert.That(() => new Role(""),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Role.Name)));
    }

    [Test]
    public void CreateRole_Input_Name_Whitespace()
    {
        Assert.That(() => new Role(" "),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(Role.Name)));
    }
}