using ContactApp.Report.Application;
using ContactApp.Report.Infrastructure;
using ContactApp.Shared.Middlewares;
using ContactApp.Shared.MongoDb.Serializers;
using ContactApp.Shared.MongoDb.Transaction;
using ContactApp.Shared.Outbox;
using CorrelationId;
using CorrelationId.DependencyInjection;

namespace ContactApp.Report.Api;

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
        serviceCollection.AddDefaultCorrelationId();

        serviceCollection
            .AddInfrastructure(configuration)
            .AddApplication()
            .AddMongoDbTransaction()
            .AddMongoDbSerializers()
            .AddOutbox(configuration);
    }
    
    public static void UseApi(this WebApplication app)
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