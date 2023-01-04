using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Adventuring.Architecture.Model.Entity.Interface;

namespace Adventuring.Architecture.Model.Entity.Implementation.Record;

/// <summary>
/// All entity models backed by SQL databases must be derived from this class.
/// </summary>
public abstract class BaseRecord : IEntity
{
    /// <inheritdoc/>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string ID { get; set; }
    /// <inheritdoc/>
    [Required]
    public DateTime CreatedAt { get; set; }
    /// <inheritdoc/>
    public string? CreatedBy { get; set; }
    /// <inheritdoc/>
    public DateTime? UpdatedAt { get; set; }
    /// <inheritdoc/>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Will generate a new ID and populate creation time with current time represented in UTC.
    /// </summary>
    public BaseRecord()
    {
        this.ID = Guid.NewGuid().ToString();
        this.CreatedAt = DateTime.UtcNow;
    }
}