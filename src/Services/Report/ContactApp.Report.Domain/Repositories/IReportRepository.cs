using ContactApp.Report.Domain.Aggregates;

namespace ContactApp.Report.Domain.Repositories;

public interface IReportRepository
{
    public Task Create(Aggregates.Report report, CancellationToken cancellationToken);
    Task<Domain.Aggregates.Report> Get(ReportId id, CancellationToken cancellationToken);
    Task AddData(Domain.Aggregates.Report report, CancellationToken cancellationToken);
}