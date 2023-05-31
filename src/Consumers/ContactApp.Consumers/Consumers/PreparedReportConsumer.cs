using ContactApp.Report.Application.Commands.AddReportData;
using ContactApp.Shared.Events;
using MassTransit;
using MediatR;

namespace ContactApp.Consumers.Consumers;

public class PreparedReportConsumer : IConsumer<PreparedReport>
{
    private readonly IMediator _mediator;
    private readonly ILogger<PreparedReportConsumer> _logger;

    public PreparedReportConsumer(IMediator mediator, ILogger<PreparedReportConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PreparedReport> context)
    {
        var data = context.Message.Data.Select(d =>
            new AddReportDataData(d.Location, d.TotalPerson, d.TotalPhoneNumber));

        await _mediator.Send(new AddReportData(context.Message.ReportId, data));
        
        _logger.LogInformation("Completed. {CorrelationId}", context.CorrelationId.GetValueOrDefault());
    }
}