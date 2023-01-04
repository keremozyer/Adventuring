using Adventuring.Architecture.AppException.Model.Derived.Validation;
using Adventuring.Contexts.UserManager.Concern.Helper;
using Adventuring.Contexts.UserManager.Model.Domain.UserAggregate;

namespace Adventuring.Contexts.UserManager.Test.Unit.User.Domain;

public class AppUserTests : BaseUserTestFixture
{
    [Test]
    public void AppUser_Create_Success()
    {
        string username = "a";
        string roleName = "b";
        string password = "Pass";

        AppUser user = new(username, password, new Role[] { new(roleName) });

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Username, Is.Not.Null);
        Assert.That(user.Username, Is.Not.Empty);
        Assert.That(user.Username, Is.EqualTo(username));
        Assert.That(user.Salt, Is.Not.Null);
        Assert.That(user.Salt, Is.Not.Empty);
        Assert.That(user.Password, Is.Not.Null);
        Assert.That(user.Password, Is.Not.Empty);
        Assert.That(user.Password, Is.Not.EqualTo(password));
        Assert.That(user.Password, Is.EqualTo(PasswordHelper.HashPassword(password, Convert.FromBase64String(user.Salt)).HashedText));
        Assert.That(user.Roles, Is.Not.Null);
        Assert.That(user.Roles, Is.Not.Empty);
        Assert.That(user.Roles, Has.Count.EqualTo(1));
        Assert.That(user.Roles.First().Name, Is.EqualTo(roleName));
    }

    [Test]
    public void CreateRole_Input_Username_Null()
    {
        Assert.That(() => new AppUser(null, "b", new Role[] { new("c") }),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Username)));
    }

    [Test]
    public void CreateRole_Input_Username_Empty()
    {
        Assert.That(() => new AppUser("", "b", new Role[] { new("c") }),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Username)));
    }

    [Test]
    public void CreateRole_Input_Username_Whitespace()
    {
        Assert.That(() => new AppUser("  ", "b", new Role[] { new("c") }),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Username)));
    }

    [Test]
    public void CreateRole_Input_Password_Null()
    {
        Assert.That(() => new AppUser("a", null, new Role[] { new("c") }),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Password)));
    }

    [Test]
    public void CreateRole_Input_Password_Empty()
    {
        Assert.That(() => new AppUser("a", "", new Role[] { new("c") }),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Password)));
    }

    [Test]
    public void CreateRole_Input_Password_Whitespace()
    {
        Assert.That(() => new AppUser("a", "  ", new Role[] { new("c") }),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Password)));
    }

    [Test]
    public void CreateRole_Input_Roles_Null()
    {
        Assert.That(() => new AppUser("a", "b", null),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Roles)));
    }

    [Test]
    public void CreateRole_Input_Roles_Empty()
    {
        Assert.That(() => new AppUser("a", "b", Array.Empty<Role>()),
            Throws.TypeOf<ValidationException>().And.Property(nameof(ValidationException.Messages)).One.Property(nameof(ValidationExceptionMessage.FieldName)).EqualTo(nameof(AppUser.Roles)));
    }
}