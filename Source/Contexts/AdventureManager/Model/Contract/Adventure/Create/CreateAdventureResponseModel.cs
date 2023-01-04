namespace Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.Create;

/// <summary>
/// Response contract for POST Adventure
/// </summary>
public class CreateAdventureResponseModel
{
    /// <summary>
    /// ID of the newly created adventure.
    /// </summary>
    public required string ID { get; set; }
}