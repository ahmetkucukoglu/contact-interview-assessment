using ContactApp.Person.Domain.Aggregates;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ContactApp.Person.Infrastructure.MongoDb;

public class PersonIdSerializer : SerializerBase<PersonId>
{
    private readonly IBsonSerializer<string> _bsonSerializer;

    public PersonIdSerializer(IBsonSerializer<string> bsonSerializer)
    {
        _bsonSerializer = bsonSerializer;
    }

    public override PersonId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return new PersonId(Guid.Parse(_bsonSerializer.Deserialize(context, args)));
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PersonId value)
    {
        _bsonSerializer.Serialize(context, args, value.Value.ToString());
    }
}