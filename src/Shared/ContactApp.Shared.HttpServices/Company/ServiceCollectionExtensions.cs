using ContactApp.Shared.HttpServices.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ContactApp.Shared.HttpServices.Company;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCompanyHttpService(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<ICompanyApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:CompanyBaseUri").Value!))
            .AddPolicyHandler(RetryPolicy.GetRetryPolicy())
            .AddHttpMessageHandler<RefitHeaderHandler>();

        return serviceCollection;
    }
}