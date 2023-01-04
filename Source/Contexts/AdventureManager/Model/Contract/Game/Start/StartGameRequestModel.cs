namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.Start;

/// <summary>
/// Request contract for POST Game
/// </summary>
public class StartGameRequestModel
{
    /// <summary>
    /// ID of the adventure tree to use in this game.
    /// </summary>
    public required string AdventureID { get; set; }
    /// <summary>
    /// Answer for the first question in the adventure. Should be set to <see langword="true"/> for the positive node and <see langword="false"/> for the negative node.
    /// </summary>
    public required bool FirstAnswer { get; set; }
}