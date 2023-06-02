using MongoDB.Driver;

namespace ContactApp.Shared.MongoDb.Transaction;

public class TransactionalManagement : ITransactionManagement
{
    private readonly IMongoClient _mongoClient;

    public TransactionalManagement(IMongoClient mongoClient)
    {
        _mongoClient = mongoClient;
    }

    public async Task Save(Func<Task> action, CancellationToken cancellationToken)
    {
        using var session = await _mongoClient.StartSessionAsync(cancellationToken: cancellationToken);
        try
        {
            session.StartTransaction();

            await action();

            await session.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await session.AbortTransactionAsync(cancellationToken);

            throw;
        }
    }
}