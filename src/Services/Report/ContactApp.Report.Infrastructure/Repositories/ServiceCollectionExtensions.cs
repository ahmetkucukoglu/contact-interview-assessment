using ContactApp.Report.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Report.Infrastructure.Repositories;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IReportRepository, ReportRepository>();

        return serviceCollection;
    }
}