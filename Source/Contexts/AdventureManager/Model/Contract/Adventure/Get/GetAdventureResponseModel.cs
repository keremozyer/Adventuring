namespace Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Get;

/// <summary>
/// Contract for GET Adventure/{ID}
/// </summary>
public class GetAdventureResponseModel
{
    /// <summary>
    /// ID of the adventure.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// Name of the adventure.
    /// </summary>
    public required string AdventureName { get; set; }
    /// <summary>
    /// Entry point for the adventure.
    /// </summary>
    public required AdventureNode StartingNode { get; set; }
}