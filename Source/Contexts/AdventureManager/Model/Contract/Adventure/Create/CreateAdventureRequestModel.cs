namespace Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Create;

/// <summary>
/// Request contract for POST Adventure
/// </summary>
public class CreateAdventureRequestModel
{
    /// <summary>
    /// Name of the adventure.
    /// </summary>
    public required string AdventureName { get; set; }
    /// <summary>
    /// Entry point for the adventure.
    /// </summary>
    public required AdventureNode StartingNode { get; set; }
}