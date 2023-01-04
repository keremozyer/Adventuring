using Adventuring.Architecture.Model.Entity.Interface;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adventuring.Architecture.Model.Entity.Base.Record;

public abstract class BaseRecord : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid ID { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    public BaseRecord()
    {
        this.ID = Guid.NewGuid();
        this.CreatedAt = DateTime.UtcNow;
    }
}