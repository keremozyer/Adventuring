namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree;

/// <summary>
/// Data class to hold a node in the adventure tree.
/// </summary>
public class AdventureNode
{
    /// <summary>
    /// Message of the node.
    /// </summary>
    public required string NodeMessage { get; set; }
    /// <summary>
    /// Next node in the positive path.
    /// </summary>
    public AdventureNode? PositiveAnswerNode { get; set; }
    /// <summary>
    /// Next node in the negative path.
    /// </summary>
    public AdventureNode? NegativeAnswerNode { get; set; }
}