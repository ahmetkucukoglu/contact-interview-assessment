using System.Text.Json;
using ContactApp.Shared.Events;

namespace ContactApp.Shared.Outbox;

public class OutboxMessage
{ 
    public string Id { get; private set; }
    public string MessageType { get; private set; }
    public string Payload { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? ProcessedAt { get; private set; }

    public OutboxMessage(string correlationId, EventBase @event)
    {
        Id = correlationId;
        MessageType = @event.GetType().AssemblyQualifiedName!;
        Payload = JsonSerializer.Serialize(@event, @event.GetType());
        CreatedAt = DateTimeOffset.Now;
    }
}