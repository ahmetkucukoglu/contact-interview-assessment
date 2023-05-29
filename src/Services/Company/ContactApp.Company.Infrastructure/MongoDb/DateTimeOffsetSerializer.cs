using System.Globalization;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ContactApp.Company.Infrastructure.MongoDb;

public class DateTimeOffsetSerializer : SerializerBase<DateTimeOffset>
{
    private readonly IBsonSerializer<string> _bsonSerializer;

    public DateTimeOffsetSerializer(IBsonSerializer<string> bsonSerializer)
    {
        _bsonSerializer = bsonSerializer;
    }

    public override DateTimeOffset Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return DateTimeOffset.Parse(_bsonSerializer.Deserialize(context, args), DateTimeFormatInfo.InvariantInfo);
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTimeOffset value)
    {
        _bsonSerializer.Serialize(context, args,
            value.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFK", DateTimeFormatInfo.InvariantInfo));
    }
}