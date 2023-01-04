namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.AdvanceGame;

/// <summary>
/// Response contract for PATCH Game.
/// </summary>
public class AdvanceGameResponseModel
{
    /// <summary>
    /// Message of the next node in the adventure tree.
    /// </summary>
    public required string NextMessage { get; set; }
    /// <summary>
    /// Will be set to <see langword="true"/> if the next node in the tree is an end node and the game is over.
    /// </summary>
    public required bool IsEndNode { get; set; }
}