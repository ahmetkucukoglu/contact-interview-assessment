using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContactApp.Shared.Outbox;

public class OutboxRepository : IOutboxRepository
{
    private readonly IMongoCollection<OutboxMessage> _collection;

    public OutboxRepository(IMongoClient mongoClient, IOptions<MongoDbOutboxSettings> mongoDbOutboxSettings)
    {
        var database = mongoClient.GetDatabase(mongoDbOutboxSettings.Value.DatabaseName);
        _collection = database.GetCollection<OutboxMessage>("outbox");
    }

    public async Task Create(OutboxMessage outbox, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(outbox, cancellationToken: cancellationToken);
    }

    public async Task<List<OutboxMessage>> GetUnProcessedMessagesAsync()
    {
        var filter = Builders<OutboxMessage>.Filter.Eq(c => c.ProcessedAt, null);
        var cursor = await _collection.FindAsync(filter);

        return await cursor.ToListAsync();
    }

    public async Task ProcessedAsync(OutboxMessage outboxMessage)
    {
        var filter = Builders<OutboxMessage>.Filter.Eq(c => c.Id, outboxMessage.Id);
        var update = Builders<OutboxMessage>.Update
            .Set(o => o.ProcessedAt, DateTimeOffset.Now);

        await _collection.UpdateOneAsync(filter, update);
    }
}