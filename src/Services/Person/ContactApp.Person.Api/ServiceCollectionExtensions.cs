using ContactApp.Person.Application;
using ContactApp.Person.Infrastructure;
using ContactApp.Shared.Middlewares;
using ContactApp.Shared.MongoDb.Serializers;

namespace ContactApp.Person.Api;

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
            .AddPersonInfrastructure(configuration)
            .AddPersonApplication();
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