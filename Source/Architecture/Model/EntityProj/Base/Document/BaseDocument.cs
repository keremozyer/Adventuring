using Adventuring.Architecture.Model.Entity.Interface;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Adventuring.Architecture.Model.Entity.Base.Document;

public abstract class BaseDocument : IEntity
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid ID { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}