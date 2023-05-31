using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ContactApp.Shared.MongoDb.Serializers;

public class GuidSerializer : SerializerBase<Guid>
{
    private readonly IBsonSerializer<string> _bsonSerializer;

    public GuidSerializer(IBsonSerializer<string> bsonSerializer)
    {
        _bsonSerializer = bsonSerializer;
    }

    public override Guid Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return Guid.Parse(_bsonSerializer.Deserialize(context, args));
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Guid value)
    {
        _bsonSerializer.Serialize(context, args, value.ToString());
    }
}