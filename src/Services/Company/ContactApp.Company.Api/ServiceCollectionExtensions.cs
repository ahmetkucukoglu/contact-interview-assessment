using ContactApp.Company.Application;
using ContactApp.Company.Infrastructure;
using ContactApp.Shared;
using ContactApp.Shared.Middlewares;
using ContactApp.Shared.MongoDb.Serializers;

namespace ContactApp.Company.Api;

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
        serviceCollection.AddGlobalExceptionHandler();

        serviceCollection
            .AddMongoDbSerializers()
            .AddCompanyInfrastructure(configuration)
            .AddCompanyApplication();
    }
    
    public static void UseApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseGlobalExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}