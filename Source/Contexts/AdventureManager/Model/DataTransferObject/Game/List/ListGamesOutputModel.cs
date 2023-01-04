namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.Game.List;

/// <summary></summary>
public class ListGamesOutputModel
{
    /// <summary>
    /// Games of the active user.
    /// </summary>
    public IReadOnlyCollection<GameModel>? Games { get; set; }
}