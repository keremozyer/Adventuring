using Adventuring.Architecture.Concern.Option.Auth;
using Adventuring.Contexts.UserManager.Test.Unit._Base;

namespace Adventuring.Contexts.UserManager.Test.Unit.Token;

public class BaseTokenTestFixture : BaseUserManagerTestFixture
{
    protected readonly TokenSettings TokenSettings = new()
    {
        ExpiresInSeconds = 60,
        SecurityKey = "keremkeremkeremkeremkeremkeremke"
    };
}