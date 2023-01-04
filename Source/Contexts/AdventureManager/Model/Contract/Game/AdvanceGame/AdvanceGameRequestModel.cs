namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.AdvanceGame;

/// <summary>
/// Request contract for PATCH Game.
/// </summary>
public class AdvanceGameRequestModel
{
    /// <summary>
    /// Game's ID.
    /// </summary>
    public required string GameID { get; set; }
    /// <summary>
    /// Choosen answer.
    /// </summary>
    public required bool ChoosenPath { get; set; }
}