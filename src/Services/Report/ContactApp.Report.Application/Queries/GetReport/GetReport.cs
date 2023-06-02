using ContactApp.Report.Domain.Aggregates;
using MediatR;

namespace ContactApp.Report.Application.Queries.GetReport;

public record GetReport(Guid Id) : IRequest<GetReportResponse>;

public record GetReportResponse
{
    public Guid Id { get; init; }
    public ReportStatuses Status { get; init; }
    public IEnumerable<GetReportData> Data { get; init; }
}

public record GetReportData
{
    public string Location { get; init; }
    public int TotalPerson { get; init; }
    public int TotalPhoneNumber { get; init; }
}