namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game;

/// <summary>
/// Game data.
/// </summary>
public class GameModel
{
    /// <summary>
    /// ID of the game.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// ID of the adventure this game occurs in.
    /// </summary>
    public required string AdventureID { get; set; }
    /// <summary>
    /// Name of the adventure this game occurs in.
    /// </summary>
    public required string AdventureName { get; set; }
    /// <summary>
    /// Will be <see langword="true"/> if the game is ended. Otherwise will be <see langword="false"/>.
    /// </summary>
    public required bool IsGameOver { get; set; }
}