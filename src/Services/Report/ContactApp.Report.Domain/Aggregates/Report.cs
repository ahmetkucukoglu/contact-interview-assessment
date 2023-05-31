using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Report.Domain.Aggregates;

public class Report : AggregateRoot<ReportId>
{
    public ReportStatuses Status { get; private set; }

    public Report()
    {
        Id = new ReportId(Guid.NewGuid());
        Status = ReportStatuses.Preparing;
    }
}