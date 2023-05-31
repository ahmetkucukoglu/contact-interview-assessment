using System.Text.Json;
using ContactApp.Shared.Outbox;
using MassTransit;

namespace ContactApp.OutboxProcessor;

public class OutboxProcessor : IHostedService
{
    private Timer _timer;
    private readonly IOutboxRepository _outboxRepository;
    private readonly IBusControl _busControl;

    public OutboxProcessor(IOutboxRepository outboxRepository, IBusControl busControl)
    {
        _outboxRepository = outboxRepository;
        _busControl = busControl;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));

        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        var unProcessedMessages = await _outboxRepository.GetUnProcessedMessagesAsync();

        foreach (var unProcessedMessage in unProcessedMessages)
        {
            var message = JsonSerializer.Deserialize(unProcessedMessage.Payload,
                Type.GetType(unProcessedMessage.MessageType)!);

            if (message == null) continue;
            
            await _busControl.Publish(message);

            await _outboxRepository.ProcessedAsync(unProcessedMessage);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
}