using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace ContactApp.Report.Infrastructure.MongoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoDbSettings>(configuration.GetRequiredSection(nameof(MongoDbSettings)));
        serviceCollection.AddSingleton<IMongoClient>(s =>
            new MongoClient(configuration.GetValue<string>("MongoDbSettings:ConnectionString"))
        );

        var stringSerializer = BsonSerializer.SerializerRegistry.GetSerializer<string>();

        BsonSerializer.RegisterSerializer(new ReportIdSerializer(stringSerializer));
        
        BsonClassMap.RegisterClassMap<Domain.Aggregates.Report>(map =>
        {
            map.AutoMap();
            map.MapField("Data");
        });

        return serviceCollection;
    }
}