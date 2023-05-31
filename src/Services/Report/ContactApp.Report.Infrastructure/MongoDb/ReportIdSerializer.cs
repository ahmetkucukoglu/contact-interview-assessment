using ContactApp.Report.Domain.Aggregates;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ContactApp.Report.Infrastructure.MongoDb;

public class ReportIdSerializer : SerializerBase<ReportId>
{
    private readonly IBsonSerializer<string> _bsonSerializer;

    public ReportIdSerializer(IBsonSerializer<string> bsonSerializer)
    {
        _bsonSerializer = bsonSerializer;
    }

    public override ReportId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return new ReportId(Guid.Parse(_bsonSerializer.Deserialize(context, args)));
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ReportId value)
    {
        _bsonSerializer.Serialize(context, args, value.Value.ToString());
    }
}