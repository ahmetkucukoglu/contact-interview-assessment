using ContactApp.Report.Domain.Aggregates;
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

    public async Task<Domain.Aggregates.Report> Get(ReportId id, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Report>.Filter.Eq(p => p.Id, id) &
                     Builders<Domain.Aggregates.Report>.Filter.Eq(p => p.IsDeleted, false);

        var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddData(Domain.Aggregates.Report report, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Report>.Filter.Eq(p => p.Id, report.Id);

        var update = Builders<Domain.Aggregates.Report>.Update
            .Set(p => p.ModifiedAt, report.ModifiedAt)
            .Set(p => p.Status, report.Status)
            .Set("Data", report.GetData().ToList());

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}