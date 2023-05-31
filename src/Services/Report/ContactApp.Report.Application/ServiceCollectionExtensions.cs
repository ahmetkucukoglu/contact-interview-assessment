using ContactApp.Report.Application.Commands.CreateReport;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Report.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReport).Assembly));

        return serviceCollection;
    }
}