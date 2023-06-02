namespace ContactApp.Shared.MongoDb.Transaction;

public class NonTransactionalManagement : ITransactionManagement
{
    public async Task Save(Func<Task> action, CancellationToken cancellationToken)
    {
        await action();
    }
}