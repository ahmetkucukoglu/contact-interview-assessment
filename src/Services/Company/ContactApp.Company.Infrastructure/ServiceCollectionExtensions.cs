using ContactApp.Company.Infrastructure.MongoDb;
using ContactApp.Company.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Company.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompanyInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddMongoDb(configuration)
            .AddRepositories();

        return serviceCollection;
    }
}