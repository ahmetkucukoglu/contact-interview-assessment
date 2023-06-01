using Refit;

namespace ContactApp.Gateway.Services.Report;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddReports(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddRefitClient<IReportApi>()
            .ConfigureHttpClient(c =>
                c.BaseAddress = new Uri(configuration.GetRequiredSection("Services:ReportBaseUri").Value!));

        return serviceCollection;
    }
}