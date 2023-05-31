namespace ContactApp.Report.Infrastructure.MongoDb;

public record MongoDbSettings
{
    public string ConnectionString { get; init; }
    public string DatabaseName { get; init; }
}