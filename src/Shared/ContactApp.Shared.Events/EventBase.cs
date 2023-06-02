namespace ContactApp.Shared.Events;

public record class EventBase
{
    public Guid CorrelationId { get; set; }
}