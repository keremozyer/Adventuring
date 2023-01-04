namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.Start;

/// <summary></summary>
public class StartGameOutputModel
{
    /// <summary>
    /// ID of the newly created game.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// Next question that should be answered. If the game is ended this property will be null.
    /// </summary>
    public string? NextMessage { get; set; }
    /// <summary>
    /// Will be <see langword="true"/> if the game is ended. Otherwise will be <see langword="false"/>.
    /// </summary>
    public required bool IsEndNode { get; set; }
}