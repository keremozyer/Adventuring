using Adventuring.Architecture.Model.Entity.Implementation.Document;

namespace Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;

/// <summary>
/// Persistence model for AdventureTree.
/// </summary>
public class AdventureTree : BaseDocument
{
    /// <summary>
    /// Name of the adventure.
    /// </summary>
    public required string AdventureName { get; set; }
    /// <summary>
    /// Entry point for the adventure.
    /// </summary>
    public required virtual AdventureNode StartingNode { get; set; }
}