namespace Adventuring.Contexts.AdventureManager.Model.DataTransferObject.AdventureTree.List;

/// <summary></summary>
public class ListAdventuresOutputModel
{
    /// <summary>
    /// Adventures in the application.
    /// </summary>
    public IReadOnlyCollection<AdventureModel>? Adventures { get; set; }
}