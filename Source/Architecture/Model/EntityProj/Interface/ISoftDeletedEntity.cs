namespace Adventuring.Architecture.Model.Entity.Interface;

public interface ISoftDeletedEntity : IEntity
{
    public DateTime? DeletedAt { get; set; }
}