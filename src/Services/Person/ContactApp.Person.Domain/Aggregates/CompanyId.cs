using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class CompanyId : GuidStronglyTypedId<CompanyId>
{
    public CompanyId(Guid value) : base(value)
    {
    }
}