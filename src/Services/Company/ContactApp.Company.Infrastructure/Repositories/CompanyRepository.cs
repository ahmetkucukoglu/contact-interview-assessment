using ContactApp.Company.Domain.Aggregates;
using ContactApp.Company.Domain.Repositories;
using ContactApp.Company.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContactApp.Company.Infrastructure.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly IMongoCollection<Domain.Aggregates.Company> _collection;

    public CompanyRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

        _collection = database.GetCollection<Domain.Aggregates.Company>("companies");
    }

    public async Task Create(Domain.Aggregates.Company company, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(company, cancellationToken: cancellationToken);
    }

    public async Task<Domain.Aggregates.Company> Get(CompanyId id, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Company>.Filter.Eq(c => c.Id, id);
        var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Domain.Aggregates.Company>> GetAll(CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Company>.Filter.Empty;
        var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await cursor.ToListAsync(cancellationToken);
    }
}