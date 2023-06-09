using ContactApp.Report.Domain.Repositories;
using ContactApp.Shared.Events;
using ContactApp.Shared.MongoDb.Transaction;
using ContactApp.Shared.Outbox;
using MediatR;

namespace ContactApp.Report.Application.Commands.CreateReport;

public class CreateReportHandler : IRequestHandler<CreateReport, CreateReportResponse>
{
    private readonly IReportRepository _reportRepository;
    private readonly IOutboxRepository _outboxRepository;
    private readonly ITransactionManagement _transactionManagement;

    public CreateReportHandler(
        IReportRepository reportRepository,
        IOutboxRepository outboxRepository,
        ITransactionManagement transactionManagement)
    {
        _reportRepository = reportRepository;
        _outboxRepository = outboxRepository;
        _transactionManagement = transactionManagement;
    }

    public async Task<CreateReportResponse> Handle(CreateReport request, CancellationToken cancellationToken)
    {
        var report = new Domain.Aggregates.Report();

        /*
         * Standalone server doesn't support transaction.
         * If you have a server without standalone, you can change non-transactional to transactional.
         * Go to ContactApp.Shared/MongoDb/ServiceCollectionExtensions
         */
        await _transactionManagement.Save(async () =>
        {
            await _reportRepository.Create(report, cancellationToken);
            await _outboxRepository.Create(
                new OutboxMessage(request.CorrelationId,
                    new CreatedReport(report.Id.Value) {CorrelationId = Guid.Parse(request.CorrelationId)}),
                cancellationToken);
        }, cancellationToken);

        return new CreateReportResponse
        {
            Id = report.Id.Value,
            Status = report.Status
        };
    }
}