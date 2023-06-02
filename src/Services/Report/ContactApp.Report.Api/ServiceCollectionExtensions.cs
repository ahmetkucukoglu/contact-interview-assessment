using ContactApp.Report.Application;
using ContactApp.Report.Infrastructure;
using ContactApp.Shared.Middlewares;
using ContactApp.Shared.MongoDb.Serializers;
using ContactApp.Shared.MongoDb.Transaction;
using ContactApp.Shared.Outbox;
using ContactApp.Shared.HttpServices.Middlewares;
using CorrelationId;
using CorrelationId.DependencyInjection;

namespace ContactApp.Report.Api;

public static class ServiceCollectionExtensions
{
    public static void AddReportApi(this IServiceCollection serviceCollection, IConfiguration configuration)
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
            .AddReportInfrastructure(configuration)
            .AddReportApplication()
            .AddMongoDbTransaction()
            .AddMongoDbSerializers()
            .AddOutbox(configuration);
    }
    
    public static void UseReportApi(this WebApplication app)
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