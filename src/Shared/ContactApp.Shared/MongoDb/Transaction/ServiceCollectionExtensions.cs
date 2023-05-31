using Microsoft.Extensions.DependencyInjection;

namespace ContactApp.Shared.MongoDb.Transaction;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDbTransaction(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITransactionManagement, NonTransactionalManagement>();
        
        return serviceCollection;
    }
}