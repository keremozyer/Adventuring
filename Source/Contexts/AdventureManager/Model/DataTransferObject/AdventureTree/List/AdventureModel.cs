namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;

/// <summary>
/// Data class to hold a adventure.
/// </summary>
public class AdventureModel
{
    /// <summary>
    /// ID of the adventure.
    /// </summary>
    public string ID { get; set; }
    /// <summary>
    /// Name of the adventure.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// First question in the adventure.
    /// </summary>
    public string StartingQuestion { get; set; }

    /// <summary></summary>
    /// <param name="id">ID of the adventure.</param>
    /// <param name="name">Name of the adventure.</param>
    /// <param name="startingQuestion">First question in the adventure.</param>
    public AdventureModel(string id, string name, string startingQuestion)
    {
        this.ID = id;
        this.Name = name;
        this.StartingQuestion = startingQuestion;
    }
}