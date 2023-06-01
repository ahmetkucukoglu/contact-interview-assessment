using ContactApp.Report.Domain.Repositories;
using MediatR;

namespace ContactApp.Report.Application.Queries.GetReports;

public class GetReportsHandler : IRequestHandler<GetReports, GetReportsResponse>
{
    private readonly IReportRepository _reportRepository;

    public GetReportsHandler(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<GetReportsResponse> Handle(GetReports request, CancellationToken cancellationToken)
    {
        var (count, reports) = await _reportRepository.GetAll(request.Page, request.Size, cancellationToken);
        var responseData = reports.Select(r => new GetReportsData
        {
            Id = r.Id.Value,
            Status = r.Status
        });

        return new GetReportsResponse(count, responseData);
    }
}