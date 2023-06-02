using ContactApp.Person.Infrastructure.Repositories;
using MediatR;

namespace ContactApp.Person.Application.Queries.GetLocationReport;

public class GetLocationReportHandler : IRequestHandler<GetLocationReport, GetLocationReportResponse>
{
    private readonly LocationReportRepository _locationReportRepository;

    public GetLocationReportHandler(LocationReportRepository locationReportRepository)
    {
        _locationReportRepository = locationReportRepository;
    }

    public async Task<GetLocationReportResponse> Handle(GetLocationReport request, CancellationToken cancellationToken)
    {
        var result = await _locationReportRepository.GetReport(cancellationToken);

        return new GetLocationReportResponse
        {
            Data = result.Data.Select(d => new GetLocationReportData
            {
                Location = d.Location,
                TotalPerson = d.TotalPerson,
                TotalPhoneNumber = d.TotalPhoneNumber
            })
        };
    }
}