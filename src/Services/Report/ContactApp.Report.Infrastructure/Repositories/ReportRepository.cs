using ContactApp.Report.Domain.Repositories;
using ContactApp.Report.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContactApp.Report.Infrastructure.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly IMongoCollection<Domain.Aggregates.Report> _collection;

    public ReportRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> mongoDbSettings)
    {
        var database = mongoClient.GetDatabase(mongoDbSettings.Value.DatabaseName);

        _collection = database.GetCollection<Domain.Aggregates.Report>("reports");
    }

    public async Task Create(Domain.Aggregates.Report report, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(report, cancellationToken: cancellationToken);
    }
}