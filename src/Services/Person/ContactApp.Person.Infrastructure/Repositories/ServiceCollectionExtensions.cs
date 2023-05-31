using ContactApp.Person.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Person.Infrastructure.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<LocationReportRepository>();

        return serviceCollection;
    }
}