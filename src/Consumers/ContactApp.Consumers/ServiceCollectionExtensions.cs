using ContactApp.Consumers.Consumers;
using ContactApp.Shared.MongoDb.Serializers;
using MassTransit;

namespace ContactApp.Consumers;

public static class ServiceCollectionExtensions
{
    public static void AddConsumers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<CreatedReportConsumer>();

            busConfigurator.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSettings = configuration.GetRequiredSection("RabbitMqSettings");

                cfg.Host(rabbitMqSettings.GetValue<string>("Host"), "/", h =>
                {
                    h.Username(rabbitMqSettings.GetValue<string>("UserName"));
                    h.Password(rabbitMqSettings.GetValue<string>("Password"));
                });

                cfg.ReceiveEndpoint("created-report", e =>
                {
                    e.ConfigureConsumer<CreatedReportConsumer>(context);
                    e.UseRetry(retryConfigurator => { retryConfigurator.Interval(5, TimeSpan.FromMinutes(1)); });
                });
            });
        });

        serviceCollection.AddMongoDbSerializers();
    }
}