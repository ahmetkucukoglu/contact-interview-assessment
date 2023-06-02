namespace ContactApp.Shared.Outbox;

public interface IOutboxRepository
{
    Task Create(OutboxMessage outbox, CancellationToken cancellationToken);
    Task<List<OutboxMessage>> GetUnProcessedMessagesAsync();
    Task ProcessedAsync(OutboxMessage outboxMessage);
}