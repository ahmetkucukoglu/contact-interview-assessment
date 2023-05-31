namespace ContactApp.Shared.Outbox;

public record MongoDbOutboxSettings
{
    public string? ConnectionString { get; init; }
    public string DatabaseName { get; init; }
}