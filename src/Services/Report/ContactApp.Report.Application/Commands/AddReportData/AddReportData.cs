using MediatR;

namespace ContactApp.Report.Application.Commands.AddReportData;

public record AddReportData(Guid ReportId, IEnumerable<AddReportDataData> Data) : IRequest;

public record AddReportDataData(string Location, int TotalPerson, int TotalPhoneNumber);