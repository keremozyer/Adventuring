namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.Start;

/// <summary>
/// Response contract for POST Game
/// </summary>
public class StartGameResponseModel
{
    /// <summary>
    /// ID of the newly created game.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// Returns the message in the second layer.
    /// </summary>
    public string? NextMessage { get; set; }
    /// <summary>
    /// If the game is ended with the initial answer given while creating it this property will be <see langword="true"/> to indicate it.
    /// </summary>
    public required bool IsEndNode { get; set; }
}