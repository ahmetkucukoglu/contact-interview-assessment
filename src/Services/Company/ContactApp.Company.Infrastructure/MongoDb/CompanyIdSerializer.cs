using ContactApp.Company.Domain.Aggregates;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ContactApp.Company.Infrastructure.MongoDb;

public class CompanyIdSerializer : SerializerBase<CompanyId>
{
    private readonly IBsonSerializer<string> _bsonSerializer;

    public CompanyIdSerializer(IBsonSerializer<string> bsonSerializer)
    {
        _bsonSerializer = bsonSerializer;
    }

    public override CompanyId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return new CompanyId(Guid.Parse(_bsonSerializer.Deserialize(context, args)));
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, CompanyId value)
    {
        _bsonSerializer.Serialize(context, args, value.Value.ToString());
    }
}