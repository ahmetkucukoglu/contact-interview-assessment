using Refit;

namespace ContactApp.Gateway.Services.Person;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersons(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<IPersonApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:PersonBaseUri").Value!));

        return serviceCollection;
    }
}