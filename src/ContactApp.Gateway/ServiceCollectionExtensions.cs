using ContactApp.Gateway.Middlewares;
using ContactApp.Shared.HttpServices.Company;
using ContactApp.Shared.HttpServices.Middlewares;
using ContactApp.Shared.HttpServices.Person;
using ContactApp.Shared.HttpServices.Report;
using CorrelationId;
using CorrelationId.DependencyInjection;

namespace ContactApp.Gateway;

public static class ServiceCollectionExtensions
{
    public static void AddGatewayApi(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(o => { o.DescribeAllParametersInCamelCase(); });
        serviceCollection.AddApiExceptionHandler();
        serviceCollection.AddDefaultCorrelationId();

        serviceCollection
            .AddRefitHeaderHandler()
            .AddCompanyHttpService(configuration)
            .AddPersonHttpService(configuration)
            .AddReportHttpService(configuration);
    }

    public static void UseGatewayApi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCorrelationId();
        app.UseApiExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}