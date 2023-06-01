namespace ContactApp.Shared.HttpServices.Report;

public record CreateReport;

public record CreateReportResponse
{
    public Guid Id { get; init; }
    public int Status { get; init; }
}