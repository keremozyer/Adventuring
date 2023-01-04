using Adventuring.Architecture.Model.Entity.Interface;

namespace Adventuring.Architecture.Model.Entity.Implementation.Record;

/// <summary>
/// All soft deleted records must be derived from this class.
/// </summary>
public abstract class BaseSoftDeletedRecord : BaseRecord, ISoftDeletedEntity
{
    /// <inheritdoc/>
    public DateTime? DeletedAt { get; set; }
}