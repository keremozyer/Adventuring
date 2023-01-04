namespace Adventuring.Architecture.Model.Entity.Interface;

/// <summary>
/// All entities with soft delete logic must implement this interface.
/// </summary>
public interface ISoftDeletedEntity : IEntity
{
    /// <summary>
    /// Indicates when this record got deleted in UTC timezone.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}