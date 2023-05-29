using ContactApp.Company.Application.Commands.CreateCompany;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Company.Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCompany).Assembly));
    }
}