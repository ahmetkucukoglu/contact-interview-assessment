using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Shared.Outbox;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOutbox(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.Configure<MongoDbOutboxSettings>(configuration.GetRequiredSection(nameof(MongoDbOutboxSettings)));

        serviceCollection.AddSingleton<IOutboxRepository, OutboxRepository>();
        
        return serviceCollection;
    }
}