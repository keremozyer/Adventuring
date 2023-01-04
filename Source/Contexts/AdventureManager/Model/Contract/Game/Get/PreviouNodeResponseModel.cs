namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.Get;

/// <summary>
/// Contract to show given answers in a game.
/// </summary>
public class PreviouNodeResponseModel
{
    /// <summary>
    /// Message of the node.
    /// </summary>
    public required string Message { get; set; }
    /// <summary>
    /// Choosen path for this node. Will be <see langword="null"/> if answer is not given yet for this node.
    /// </summary>
    public bool? ChoosenAnswer { get; set; }
    /// <summary>
    /// Order of this node in the game.
    /// </summary>
    public required int Order { get; set; }
}