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
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.Id, id) &
                     Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.IsDeleted, false);

        var cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);

        return await cursor.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task Delete(Domain.Aggregates.Person person, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.Id, person.Id);

        var update = Builders<Domain.Aggregates.Person>.Update
            .Set(p => p.ModifiedAt, DateTimeOffset.Now)
            .Set(p => p.IsDeleted, person.IsDeleted);

        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }

    public async Task AddContacts(Domain.Aggregates.Person person, CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.Id, person.Id);

        var update = Builders<Domain.Aggregates.Person>.Update
            .Set(p => p.ModifiedAt, DateTimeOffset.Now)
            .PushEach("Contacts", person.GetAddedNewContacts());

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

    public async Task<(int Count, List<Domain.Aggregates.Person>)> GetAll(int page, int size,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Domain.Aggregates.Person>.Filter.Eq(p => p.IsDeleted, false);

        var count = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        var skip = page == 1 ? 0 : (page - 1) * size;

        var cursor = await _collection.FindAsync(filter, new FindOptions<Domain.Aggregates.Person>
        {
            Sort = Builders<Domain.Aggregates.Person>.Sort.Ascending(p => p.FirstName).Ascending(p => p.LastName),
            Skip = skip,
            Limit = size
        }, cancellationToken: cancellationToken);

        return ((int) count, await cursor.ToListAsync(cancellationToken));
    }
}