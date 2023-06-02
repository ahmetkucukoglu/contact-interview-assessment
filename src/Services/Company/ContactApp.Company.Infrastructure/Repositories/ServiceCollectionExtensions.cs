using ContactApp.Company.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Company.Infrastructure.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<ICompanyRepository, CompanyRepository>();

        return serviceCollection;
    }
}