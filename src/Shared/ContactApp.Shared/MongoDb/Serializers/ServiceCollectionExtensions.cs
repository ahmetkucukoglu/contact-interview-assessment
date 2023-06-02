using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace ContactApp.Shared.MongoDb.Serializers;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbSerializers(this IServiceCollection serviceCollection)
    {
        var stringSerializer = BsonSerializer.SerializerRegistry.GetSerializer<string>();

        BsonSerializer.RegisterSerializer(new GuidSerializer(stringSerializer));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(stringSerializer));

        return serviceCollection;
    }
}