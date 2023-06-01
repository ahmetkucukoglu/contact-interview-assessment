using ContactApp.Shared.HttpServices.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ContactApp.Shared.HttpServices.Person;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonHttpService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<IPersonApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:PersonBaseUri").Value!))
            .AddPolicyHandler(RetryPolicy.GetRetryPolicy())
            .AddHttpMessageHandler<RefitHeaderHandler>();

        return serviceCollection;
    }
}