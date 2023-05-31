using ContactApp.Report.Infrastructure.MongoDb;
using ContactApp.Report.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Report.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddMongoDb(configuration)
            .AddRepositories();

        return serviceCollection;
    }
}