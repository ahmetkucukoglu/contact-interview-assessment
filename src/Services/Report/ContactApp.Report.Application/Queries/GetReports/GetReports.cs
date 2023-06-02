using ContactApp.Report.Domain.Aggregates;
using MediatR;

namespace ContactApp.Report.Application.Queries.GetReports;

public record GetReports(int Page = 1, int Size = 10) : IRequest<GetReportsResponse>;

public record GetReportsResponse(int Count, IEnumerable<GetReportsData> Data);

public record GetReportsData
{
    public Guid Id { get; init; }
    public ReportStatuses Status { get; init; }
}