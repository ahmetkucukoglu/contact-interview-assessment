using ContactApp.Person.Application.Commands.CreatePerson;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Person.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePerson).Assembly));
    }
}