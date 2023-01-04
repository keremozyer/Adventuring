namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.AdvanceGame;

/// <summary></summary>
public class AdvanceGameInputModel
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