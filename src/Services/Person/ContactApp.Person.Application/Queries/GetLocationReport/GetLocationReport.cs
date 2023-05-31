using MediatR;

namespace ContactApp.Person.Application.Queries.GetLocationReport;

public record GetLocationReport : IRequest<GetLocationReportResponse>;

public record GetLocationReportResponse
{
    public IEnumerable<GetLocationReportData> Data { get; init; }
}

public record GetLocationReportData
{
    public string Location { get; init; }
    public int TotalPerson { get; init; }
    public int TotalPhoneNumber { get; init; }
}