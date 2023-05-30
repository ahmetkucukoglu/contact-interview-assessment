using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace ContactApp.Person.Infrastructure.MongoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoDbSettings>(configuration.GetRequiredSection(nameof(MongoDbSettings)));

        var stringSerializer = BsonSerializer.SerializerRegistry.GetSerializer<string>();

        BsonSerializer.RegisterSerializer(new PersonIdSerializer(stringSerializer));
        BsonSerializer.RegisterSerializer(new CompanyIdSerializer(stringSerializer));
        BsonSerializer.RegisterSerializer(new GuidSerializer(stringSerializer));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(stringSerializer));
        
        BsonClassMap.RegisterClassMap<Domain.Aggregates.Person>(map =>
        {
            map.AutoMap();
            map.MapField("Contacts");
            map.UnmapField("AddedNewContacts");
        });

        return serviceCollection;
    }
}