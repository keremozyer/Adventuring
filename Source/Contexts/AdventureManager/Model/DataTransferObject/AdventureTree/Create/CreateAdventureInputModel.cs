namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.Create;

/// <summary></summary>
public class CreateAdventureInputModel
{
    /// <summary>
    /// Name of the new adventure.
    /// </summary>
    public required string AdventureName { get; set; }
    /// <summary>
    /// Entry point for the new adventure tree.
    /// </summary>
    public required AdventureNode StartingNode { get; set; }
}