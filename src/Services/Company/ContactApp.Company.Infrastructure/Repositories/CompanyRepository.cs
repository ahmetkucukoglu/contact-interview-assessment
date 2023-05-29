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
}