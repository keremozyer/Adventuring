namespace Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.List;

/// <summary>
/// Response contract for GET Adventure
/// </summary>
public class ListAdventuresResponseModel
{
    /// <summary>
    /// Adventures in the application.
    /// </summary>
    public IReadOnlyCollection<AdventureResponseModel>? Adventures { get; set; }
}