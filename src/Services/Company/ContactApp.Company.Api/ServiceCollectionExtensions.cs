using ContactApp.Company.Application;
using ContactApp.Company.Infrastructure;
using ContactApp.Shared.Middlewares;
using ContactApp.Shared.MongoDb.Serializers;
using CorrelationId;
using CorrelationId.DependencyInjection;

namespace ContactApp.Company.Api;

public static class ServiceCollectionExtensions
{
    public static void AddCompanyApi(this IServiceCollection serviceCollection, IConfiguration configuration)
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
            .AddMongoDbSerializers()
            .AddCompanyInfrastructure(configuration)
            .AddCompanyApplication();
    }
    
    public static void UseCompanyApi(this WebApplication app)
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