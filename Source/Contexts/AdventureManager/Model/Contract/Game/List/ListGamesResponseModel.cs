namespace Adventuring.Contexts.AdventureManager.Model.Contract.Game.List;

/// <summary>
/// Response contract for GET Game
/// </summary>
public class ListGamesResponseModel
{
    /// <summary>
    /// Games of the active user.
    /// </summary>
    public IReadOnlyCollection<GameResponseModel>? Games { get; set; }
}