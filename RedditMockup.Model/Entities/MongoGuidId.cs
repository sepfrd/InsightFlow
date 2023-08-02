using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RedditMockup.Model.Entities;

public class MongoGuidId
{
    public MongoGuidId(Guid guid, int id)
    {
        Guid = guid;
        Id = id;
    }
    
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ObjectId { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid Guid { get; set; }
    
    public int Id { get; set; }
}