using ContactApp.Shared.MongoDb.Serializers;
using ContactApp.Shared.Outbox;
using MassTransit;
using MongoDB.Driver;

namespace ContactApp.OutboxProcessor;

public static class ServiceCollectionExtensions
{
    public static void AddProcessor(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddSingleton<IMongoClient>(s =>
            new MongoClient(configuration.GetValue<string>("MongoDbOutboxSettings:ConnectionString"))
        );
        serviceCollection.AddHostedService<OutboxProcessor>();

        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = configuration.GetRequiredSection("RabbitMqSettings");

                cfg.Host(rabbitMqSettings.GetValue<string>("Host"), "/", h =>
                {
                    h.Username(rabbitMqSettings.GetValue<string>("UserName"));
                    h.Password(rabbitMqSettings.GetValue<string>("Password"));
                });
            });
        });

        serviceCollection
            .AddMongoDbSerializers()
            .AddOutbox(configuration);
    }
}