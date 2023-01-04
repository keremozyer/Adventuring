using Adventuring.Architecture.Model.Entity.Implementation.Document;

namespace Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;

/// <summary>
/// Persistance model for adventure nodes in the tree.
/// </summary>
public class AdventureNode : BaseDocument
{
    /// <summary>
    /// Message of the node.
    /// </summary>
    public required string NodeMessage { get; set; }
    /// <summary>
    /// Next node in the positive path.
    /// </summary>
    public virtual AdventureNode? PositiveAnswerNode { get; set; }
    /// <summary>
    /// Next node in the negative path.
    /// </summary>
    public virtual AdventureNode? NegativeAnswerNode { get; set; }
}