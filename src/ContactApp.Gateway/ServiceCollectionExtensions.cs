using ContactApp.Gateway.Middlewares;
using ContactApp.Gateway.Services.Company;
using ContactApp.Gateway.Services.Person;
using ContactApp.Gateway.Services.Report;
using CorrelationId;
using CorrelationId.DependencyInjection;

namespace ContactApp.Gateway;

public static class ServiceCollectionExtensions
{
    public static void AddGatewayApi(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(o =>
        {
            o.DescribeAllParametersInCamelCase();
        });
        serviceCollection.AddApiExceptionHandler();
        serviceCollection.AddDefaultCorrelationId();

        serviceCollection
            .AddRefitHeaderHandler()
            .AddCompanies(configuration)
            .AddPersons(configuration)
            .AddReports(configuration);
    }
    
    public static void UseGatewayApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCorrelationId();
        app.UseApiExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}