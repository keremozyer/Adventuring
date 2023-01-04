namespace Adventuring.Architecture.Concern.Constant.Auth;

/// <summary>
/// Key values to create claims in JWT tokens.
/// </summary>
public static class UserClaimKeys
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public const string ID = nameof(ID);
    public const string Role = nameof(Role);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}