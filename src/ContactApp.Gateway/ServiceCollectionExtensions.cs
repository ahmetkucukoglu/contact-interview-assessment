using ContactApp.Gateway.Middlewares;
using ContactApp.Gateway.Services.Company;
using ContactApp.Gateway.Services.Person;
using ContactApp.Gateway.Services.Report;

namespace ContactApp.Gateway;

public static class ServiceCollectionExtensions
{
    public static void AddApi(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(o =>
        {
            o.DescribeAllParametersInCamelCase();
        });
        serviceCollection.AddApiExceptionHandler();

        serviceCollection
            .AddCompanies(configuration)
            .AddPersons(configuration)
            .AddReports(configuration);
    }
    
    public static void UseApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseApiExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}