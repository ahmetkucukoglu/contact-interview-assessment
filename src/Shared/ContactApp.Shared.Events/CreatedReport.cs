namespace ContactApp.Shared.Events;

public record CreatedReport(Guid ReportId) : EventBase;