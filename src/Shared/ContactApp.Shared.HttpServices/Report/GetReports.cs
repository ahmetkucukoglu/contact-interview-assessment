namespace ContactApp.Shared.HttpServices.Report;

public record GetReports(int Page = 1, int Size = 10);

public record GetReportsResponse(int Count, IEnumerable<GetReportsData> Data);

public record GetReportsData
{
    public Guid Id { get; init; }
    public int Status { get; init; }
}