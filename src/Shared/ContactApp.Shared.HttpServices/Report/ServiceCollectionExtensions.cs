using ContactApp.Shared.HttpServices.Middlewares;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ContactApp.Shared.HttpServices.Report;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReportHttpService(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<IReportApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:ReportBaseUri").Value!))
            .AddPolicyHandler(RetryPolicy.GetRetryPolicy())
            .AddHttpMessageHandler<RefitHeaderHandler>();

        return serviceCollection;
    }
}