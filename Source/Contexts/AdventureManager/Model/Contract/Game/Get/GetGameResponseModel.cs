namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.Get;

/// <summary>
/// Response contract for GET Game/{ID}
/// </summary>
public class GetGameResponseModel
{
    /// <summary>
    /// Metadata for the game.
    /// </summary>
    public required GameResponseModel GameData { get; set; }
    /// <summary>
    /// Given answers for this game.
    /// </summary>
    public required IReadOnlyCollection<PreviouNodeResponseModel> PreviousAnswers { get; set; }
    /// <summary>
    /// Message of the current node. If the game is ended and there are no questions to answer this property will be null.
    /// </summary>
    public string? CurrentMessage { get; set; }
}