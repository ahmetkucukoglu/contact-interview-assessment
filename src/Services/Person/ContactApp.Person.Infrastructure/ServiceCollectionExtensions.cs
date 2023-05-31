using ContactApp.Person.Infrastructure.MongoDb;
using ContactApp.Person.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Person.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddMongoDb(configuration)
            .AddRepositories();

        return serviceCollection;
    }
}