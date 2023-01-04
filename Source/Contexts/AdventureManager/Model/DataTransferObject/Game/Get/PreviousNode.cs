namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;

/// <summary>
/// DTO class to hold given answers in a game.
/// </summary>
public class PreviousNode
{
    /// <summary>
    /// Message of the node.
    /// </summary>
    public string Message { get; set; }
    /// <summary>
    /// Choosen path for this node. Will be <see langword="null"/> if answer is not given yet for this node.
    /// </summary>
    public bool? ChoosenAnswer { get; set; }
    /// <summary>
    /// Order of this node in the game.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message">Message of the node.</param>
    /// <param name="choosenAnswer">Choosen path for this node. Should be <see langword="null"/> if answer is not given yet for this node.</param>
    /// <param name="order">Order of this node in the game.</param>
    public PreviousNode(string message, bool? choosenAnswer, int order)
    {
        this.Message = message;
        this.ChoosenAnswer = choosenAnswer;
        this.Order = order;
    }
}