namespace ContactApp.Shared.Events;

public record PreparedReport(Guid ReportId, IEnumerable<PreparedReportData> Data) : EventBase;
public record PreparedReportData(string Location, int TotalPerson, int TotalPhoneNumber);