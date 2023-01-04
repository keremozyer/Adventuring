using Adventuring.Architecture.Model.Entity.Implementation.Document;

namespace Adventuring.Contexts.AdventureManager.Model.Entity._Game;

/// <summary>
/// Persistence model for Game entity.
/// </summary>
public class Game : BaseDocument
{
    /// <summary>
    /// Player who plays the game.
    /// </summary>
    public required string PlayerName { get; set; }
    /// <summary>
    /// Adventure's id this game occurs in.
    /// </summary>
    public required string AdventureID { get; set; }
    /// <summary>
    /// Previously given answers in this game.
    /// </summary>
    public required IList<bool> Answers { get; set; }
    /// <summary>
    /// Will be set to <see langword="true"/> if the game is ended.
    /// </summary>
    public required bool IsGameOver { get; set; }
}