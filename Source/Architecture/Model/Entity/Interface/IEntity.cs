namespace Adventuring.Architecture.Model.Entity.Interface;

/// <summary>
/// All entities stored in a persistent data storage must implement this interface.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// ID of the entity.
    /// </summary>
    public string ID { get; set; }
    /// <summary>
    /// Entity's creation timestamp in UTC timezone.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// ID of the user who created the entity.
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// Entity's last update timestamp in UTC timezone.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
    /// <summary>
    /// ID of the user who last updated the entity.
    /// </summary>
    public string? UpdatedBy { get; set; }
}