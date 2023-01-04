using Adventuring.Architecture.Container.ActiveUser.Implementation.TestUser;

namespace Adventuring.Architecture.Test.Base.Unit;

public abstract class BaseTestFixture
{
    protected ActiveTestUser ActiveUser;

    public BaseTestFixture()
    {
        SetActiveUserToDefaultUser();
    }

    protected void SetActiveUserToDefaultUser()
    {
        SetActiveUserToAdmin();
    }

    protected void SetActiveUserToPlayer()
    {
        this.ActiveUser = new ActiveTestUser("testPlayerID", new string[] { "Player" }, "testPlayerName");
    }

    protected void SetActiveUserToDesigner()
    {
        this.ActiveUser = new ActiveTestUser("testDesignerID", new string[] { "Player", "Designer" }, "testDesignerName");
    }

    protected void SetActiveUserToAdmin()
    {
        this.ActiveUser = new ActiveTestUser("testAdminID", new string[] { "Player", "Designer", "Admin" }, "testAdminName");
    }
}