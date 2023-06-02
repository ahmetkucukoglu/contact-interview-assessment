using ContactApp.Report.Domain.Aggregates;
using MediatR;

namespace ContactApp.Report.Application.Commands.CreateReport;

public record CreateReport(string CorrelationId) : IRequest<CreateReportResponse>;

public record CreateReportResponse
{
    public Guid Id { get; init; }
    public ReportStatuses Status { get; init; }
}