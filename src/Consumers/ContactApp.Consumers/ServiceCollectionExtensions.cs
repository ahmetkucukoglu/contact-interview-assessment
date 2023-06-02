using ContactApp.Consumers.Consumers;
using ContactApp.Person.Application;
using ContactApp.Person.Infrastructure;
using ContactApp.Report.Application;
using ContactApp.Report.Infrastructure;
using ContactApp.Shared.HttpServices.Company;
using ContactApp.Shared.MongoDb.Serializers;
using ContactApp.Shared.MongoDb.Transaction;
using ContactApp.Shared.Outbox;
using MassTransit;

namespace ContactApp.Consumers;

public static class ServiceCollectionExtensions
{
    public static void AddConsumers(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddMassTransit(busConfigurator =>
        {
            busConfigurator.AddConsumer<CreatedReportConsumer>();
            busConfigurator.AddConsumer<PreparedReportConsumer>();

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
                    e.UseRetry(retryConfigurator => { retryConfigurator.Interval(5, TimeSpan.FromMinutes(5)); });
                });

                cfg.ReceiveEndpoint("prepared-report", e =>
                {
                    e.ConfigureConsumer<PreparedReportConsumer>(context);
                    e.UseRetry(retryConfigurator => { retryConfigurator.Interval(5, TimeSpan.FromMinutes(5)); });
                });
            });
        });

        serviceCollection
            .AddMongoDbSerializers()
            .AddMongoDbTransaction()
            .AddOutbox(configuration)
            .AddPersonInfrastructure(configuration)
            .AddReportInfrastructure(configuration)
            .AddPersonApplication()
            .AddReportApplication()
            .AddCompanyHttpService(configuration);
    }
}