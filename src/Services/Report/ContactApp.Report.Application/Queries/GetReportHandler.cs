using ContactApp.Report.Domain.Aggregates;
using ContactApp.Report.Domain.Repositories;
using ContactApp.Shared.Abstractions.DDD;
using MediatR;

namespace ContactApp.Report.Application.Queries;

public class GetReportHandler : IRequestHandler<GetReport, GetReportResponse>
{
    private readonly IReportRepository _reportRepository;

    public GetReportHandler(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<GetReportResponse> Handle(GetReport request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.Get(new ReportId(request.Id), cancellationToken);

        if (report == null) throw new DomainException("Report not found");

        var data = report.GetData().Select(d => new GetReportData
        {
            Location = d.Location,
            TotalPerson = d.TotalPerson,
            TotalPhoneNumber = d.TotalPhoneNumber
        });

        return new GetReportResponse
        {
            Id = report.Id.Value,
            Status = report.Status,
            Data = data
        };
    }
}