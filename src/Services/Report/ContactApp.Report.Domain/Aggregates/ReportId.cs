using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Report.Domain.Aggregates;

public class ReportId : GuidStronglyTypedId<ReportId>
{
    public ReportId(Guid value) : base(value)
    {
    }
}