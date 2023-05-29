using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Company.Domain.Aggregates;

public class CompanyId : GuidStronglyTypedId<CompanyId>
{
    public CompanyId(Guid value) : base(value)
    {
    }
}