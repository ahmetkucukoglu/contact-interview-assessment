using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class Person : AggregateRoot<PersonId>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public CompanyId CompanyId { get; private set; }

    public Person(string firstName, string lastName, CompanyId companyId)
    {
        ArgumentException.ThrowIfNullOrEmpty(firstName);
        ArgumentException.ThrowIfNullOrEmpty(lastName);

        Id = new PersonId(Guid.NewGuid());
        CompanyId = companyId;
        FirstName = firstName;
        LastName = lastName;
    }
}