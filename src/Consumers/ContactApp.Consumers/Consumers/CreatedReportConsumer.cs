using ContactApp.Shared.Events;
using MassTransit;

namespace ContactApp.Consumers.Consumers;

public class CreatedReportConsumer : IConsumer<CreatedReport>
{
    public Task Consume(ConsumeContext<CreatedReport> context)
    {
        return Task.CompletedTask;
    }
}