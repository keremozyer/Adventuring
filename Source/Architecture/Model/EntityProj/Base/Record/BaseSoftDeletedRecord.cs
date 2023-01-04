using Adventuring.Architecture.Model.Entity.Interface;

namespace Adventuring.Architecture.Model.Entity.Base.Record;

public abstract class BaseSoftDeletedRecord : BaseRecord, ISoftDeletedEntity
{
    public DateTime? DeletedAt { get; set; }
}