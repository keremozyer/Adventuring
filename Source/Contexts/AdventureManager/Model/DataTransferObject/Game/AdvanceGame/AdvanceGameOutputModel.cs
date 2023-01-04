namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;

/// <summary></summary>
public class AdvanceGameOutputModel
{
    /// <summary>
    /// Message of the next node in the adventure tree.
    /// </summary>
    public string NextMessage { get; set; }
    /// <summary>
    /// Will be set to <see langword="true"/> if the next node in the tree is an end node and the game is over.
    /// </summary>
    public bool IsEndNode { get; set; }

    /// <summary></summary>
    /// <param name="nextMessage">Message of the next node in the adventure tree.</param>
    /// <param name="isEndNode">Should be <see langword="true"/> if the next node in the tree is an end node and the game is over.</param>
    public AdvanceGameOutputModel(string nextMessage, bool isEndNode)
    {
        this.NextMessage = nextMessage;
        this.IsEndNode = isEndNode;
    }
}