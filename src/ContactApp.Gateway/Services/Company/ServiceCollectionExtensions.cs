using ContactApp.Gateway.Middlewares;
using Refit;

namespace ContactApp.Gateway.Services.Company;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompanies(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<ICompanyApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:CompanyBaseUri").Value!))
            .AddHttpMessageHandler<RefitHeaderHandler>();

        return serviceCollection;
    }
}