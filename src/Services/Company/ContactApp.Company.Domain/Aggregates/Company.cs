using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Company.Domain.Aggregates;

public class Company : AggregateRoot<CompanyId>
{
    public string Name { get; private set; }

    public Company(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Id = new CompanyId(Guid.NewGuid());
        Name = name;
    }
}