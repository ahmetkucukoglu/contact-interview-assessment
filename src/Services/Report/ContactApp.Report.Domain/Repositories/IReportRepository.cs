namespace ContactApp.Report.Domain.Repositories;

public interface IReportRepository
{
    public Task Create(Aggregates.Report report, CancellationToken cancellationToken);
}