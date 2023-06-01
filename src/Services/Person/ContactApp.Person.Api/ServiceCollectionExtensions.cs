using ContactApp.Person.Application;
using ContactApp.Person.Infrastructure;
using ContactApp.Shared.Middlewares;
using ContactApp.Shared.MongoDb.Serializers;
using ContactApp.Shared.HttpServices.Company;
using ContactApp.Shared.HttpServices.Middlewares;
using CorrelationId;
using CorrelationId.DependencyInjection;

namespace ContactApp.Person.Api;

public static class ServiceCollectionExtensions
{
    public static void AddPersonApi(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen(o =>
        {
            o.DescribeAllParametersInCamelCase();
        });
        serviceCollection.AddDefaultCorrelationId();

        serviceCollection
            .AddGlobalExceptionHandler()
            .AddRefitHeaderHandler()
            .AddMongoDbSerializers()
            .AddPersonInfrastructure(configuration)
            .AddPersonApplication()
            .AddCompanyHttpService(configuration);
    }
    
    public static void UsePersonApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseCorrelationId();
        app.UseGlobalExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}