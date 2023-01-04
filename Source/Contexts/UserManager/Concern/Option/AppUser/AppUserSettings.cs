using Adventuring.Architecture.Model.Interface.ServiceTypes;

namespace Adventuring.Contexts.UserManager.Concern.Option.AppUser;

/// <summary></summary>
public class AppUserSettings : IOptionService
{
    /// <summary>
    /// Roles that cannot be granted to any user. These will be used to seed administrator users when creating the application.
    /// </summary>
    public required string[] UngrantableRoles { get; set; }
}