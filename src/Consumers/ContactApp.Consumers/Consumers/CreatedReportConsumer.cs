using ContactApp.Person.Application.Queries.GetLocationReport;
using ContactApp.Shared.Events;
using MassTransit;
using MediatR;

namespace ContactApp.Consumers.Consumers;

public class CreatedReportConsumer : IConsumer<CreatedReport>
{
    private readonly IMediator _mediator;
    private readonly IBusControl _busControl;
    private readonly ILogger<CreatedReportConsumer> _logger;

    public CreatedReportConsumer(IMediator mediator, IBusControl busControl, ILogger<CreatedReportConsumer> logger)
    {
        _mediator = mediator;
        _busControl = busControl;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreatedReport> context)
    {
        var result = await _mediator.Send(new GetLocationReport());

        var data = result.Data.Select(d => new PreparedReportData(d.Location, d.TotalPerson, d.TotalPhoneNumber));
        var message = new PreparedReport(context.Message.ReportId, data)
            {CorrelationId = context.CorrelationId.GetValueOrDefault()};

        await _busControl.Publish(message);
        
        _logger.LogInformation("Completed. {CorrelationId}", context.CorrelationId.GetValueOrDefault());
    }
}