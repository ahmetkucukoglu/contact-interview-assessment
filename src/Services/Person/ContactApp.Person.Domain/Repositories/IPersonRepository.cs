using ContactApp.Person.Domain.Aggregates;

namespace ContactApp.Person.Domain.Repositories;

public interface IPersonRepository
{
    public Task Create(Aggregates.Person person, CancellationToken cancellationToken);
    public Task<Aggregates.Person> Get(PersonId id, CancellationToken cancellationToken);
    public Task Delete(Aggregates.Person person, CancellationToken cancellationToken);
    public Task AddContacts(Aggregates.Person person, CancellationToken cancellationToken);
    public Task RemoveContacts(Aggregates.Person person, CancellationToken cancellationToken);
    public Task<(int Count, List<Domain.Aggregates.Person>)> GetAll(int page, int size,
        CancellationToken cancellationToken);
}