namespace ContactApp.Shared.MongoDb.Transaction;

public interface ITransactionManagement
{
    Task Save(Func<Task> action, CancellationToken cancellationToken);
}