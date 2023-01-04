namespace Adventuring.Contexts.AdventureManager.Model.Contract.Adventure.List;

/// <summary></summary>
public class AdventureResponseModel
{
    /// <summary>
    /// ID of the adventure.
    /// </summary>
    public required string ID { get; set; }
    /// <summary>
    /// Name of the adventure.
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// First question in the adventure.
    /// </summary>
    public required string StartingQuestion { get; set; }
}