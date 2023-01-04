using Adventuring.Architecture.Model.Entity.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Adventuring.Architecture.Model.Entity.Implementation.Document;

/// <summary>
/// All entity models backed by MongoDB must be derived from this class.
/// </summary>
public abstract class BaseDocument : IEntity
{
    /// <inheritdoc/>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    /// <inheritdoc/>
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
    public BaseDocument()
    {
        this.ID = ObjectId.GenerateNewId().ToString();
        this.CreatedAt = DateTime.UtcNow;
    }
}