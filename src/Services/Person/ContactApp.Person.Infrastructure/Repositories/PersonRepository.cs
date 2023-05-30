using ContactApp.Person.Domain.Repositories;
using ContactApp.Person.Infrastructure.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ContactApp.Person.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IMongoCollection<Domain.Aggregates.Person> _collection;

    public PersonRepository(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);

        _collection = database.GetCollection<Domain.Aggregates.Person>("persons");
    }

    public async Task Create(Domain.Aggregates.Person person, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(person, cancellationToken: cancellationToken);
    }
}