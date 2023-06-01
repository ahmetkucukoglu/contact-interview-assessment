namespace ContactApp.Gateway.Services.Report;

public record CreateReport;

public record CreateReportResponse
{
    public Guid Id { get; init; }
    public int Status { get; init; }
}