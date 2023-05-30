namespace ContactApp.Person.Domain.Repositories;

public interface IPersonRepository
{
    public Task Create(Aggregates.Person person, CancellationToken cancellationToken);
}