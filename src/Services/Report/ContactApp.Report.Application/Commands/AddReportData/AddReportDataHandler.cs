using ContactApp.Report.Domain.Aggregates;
using ContactApp.Report.Domain.Repositories;
using MediatR;

namespace ContactApp.Report.Application.Commands.AddReportData;

public class AddReportDataHandler : IRequestHandler<AddReportData>
{
    private readonly IReportRepository _reportRepository;

    public AddReportDataHandler(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task Handle(AddReportData request, CancellationToken cancellationToken)
    {
        var report = await _reportRepository.Get(new ReportId(request.ReportId), cancellationToken);

        report.AddData(request.Data.Select(d =>
            new ReportData(d.Location, d.TotalPerson, d.TotalPhoneNumber)).ToList());

        await _reportRepository.AddData(report, cancellationToken);
    }
}