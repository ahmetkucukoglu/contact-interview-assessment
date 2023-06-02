using ContactApp.Person.Application.Commands.CreatePerson;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Person.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersonApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePerson).Assembly));

        return serviceCollection;
    }
}