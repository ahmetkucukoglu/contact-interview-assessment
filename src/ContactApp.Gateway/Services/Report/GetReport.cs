namespace ContactApp.Gateway.Services.Report;

public record GetReport(Guid Id);

public record GetReportResponse
{
    public Guid Id { get; init; }
    public int Status { get; init; }
    public IEnumerable<GetReportData> Data { get; init; }
}

public record GetReportData
{
    public string Location { get; init; }
    public int TotalPerson { get; init; }
    public int TotalPhoneNumber { get; init; }
}