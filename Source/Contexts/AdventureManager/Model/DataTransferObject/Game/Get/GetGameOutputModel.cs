namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Get;

/// <summary></summary>
public class GetGameOutputModel
{
    /// <summary>
    /// Metadata for the game.
    /// </summary>
    public required GameModel GameData { get; set; }
    /// <summary>
    /// Given answers for this game.
    /// </summary>
    public required IReadOnlyCollection<PreviousNode> PreviousAnswers { get; set; }
    /// <summary>
    /// Message of the current node. If the game is ended and there are no questions to answer this property will be null.
    /// </summary>
    public string? CurrentMessage { get; set; }
}