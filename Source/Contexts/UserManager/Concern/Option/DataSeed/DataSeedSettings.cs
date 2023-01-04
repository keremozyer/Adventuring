using Adventuring.Architecture.Model.Interface.ServiceTypes;

namespace Adventuring.Contexts.UserManager.Concern.Option.DataSeed;

/// <summary>
/// Values to seed the database when creating the application.
/// </summary>
public class DataSeedSettings : IOptionService
{
    /// <summary>
    /// Default users.
    /// </summary>
    public required IReadOnlyCollection<UserSetting> Users { get; set; }
}