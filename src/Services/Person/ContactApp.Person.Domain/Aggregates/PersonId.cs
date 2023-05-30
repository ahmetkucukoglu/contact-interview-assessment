using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class PersonId : GuidStronglyTypedId<PersonId>
{
    public PersonId(Guid value) : base(value)
    {
    }
}