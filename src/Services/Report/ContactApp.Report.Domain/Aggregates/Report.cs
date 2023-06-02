using System.Collections.ObjectModel;
using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Report.Domain.Aggregates;

public class Report : AggregateRoot<ReportId>
{
    public ReportStatuses Status { get; private set; }

    private List<ReportData> Data = new();

    public Report()
    {
        Id = new ReportId(Guid.NewGuid());
        Status = ReportStatuses.Preparing;
    }

    public void AddData(List<ReportData> data)
    {
        if (Status != ReportStatuses.Preparing) throw new DomainException("Data can not add while status is prepared");

        Data = data;
        Status = ReportStatuses.Prepared;
        ModifiedAt = DateTimeOffset.Now;
    }

    public ReadOnlyCollection<ReportData> GetData() => Data.AsReadOnly();
}