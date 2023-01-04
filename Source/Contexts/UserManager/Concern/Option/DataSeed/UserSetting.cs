namespace Adventuring.Contexts.UserManager.Concern.Option.DataSeed;

/// <summary>
/// User to create in application start.
/// </summary>
public class UserSetting
{
    /// <summary>
    /// Name of the user.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Plain text password of the user.
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// Initial roles of the user.
    /// </summary>
    public required string[] Roles { get; set; }
}