using ContactApp.Person.Domain.Aggregates;
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

    public async Task<Domain.Aggregates.Person> Get(PersonId id, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.Id, id);
        var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task AddContacts(Domain.Aggregates.Person person, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.Id, person.Id);

        var update = Builders<Domain.Aggregates.Person>.Update
            .Set(p => p.ModifiedAt, DateTimeOffset.Now);

        foreach (var contact in person.GetAddedNewContacts())
        {
            update = update.Push("Contacts", contact);
        }

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task RemoveContacts(Domain.Aggregates.Person person, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.Id, person.Id);

        var update = Builders<Domain.Aggregates.Person>.Update
            .Set(p => p.ModifiedAt, DateTimeOffset.Now);

        foreach (var contact in person.GetRemovedNewContacts())
        {
            update = update.PullFilter("Contacts", Builders<Contact>.Filter.Eq(b => b.Id, contact.Id));
        }

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}