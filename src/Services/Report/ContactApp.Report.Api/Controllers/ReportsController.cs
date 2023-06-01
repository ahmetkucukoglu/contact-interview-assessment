using ContactApp.Report.Application.Commands.CreateReport;
using ContactApp.Report.Application.Queries;
using CorrelationId.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Report.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICorrelationContextAccessor _correlationContextAccessor;

    public ReportsController(IMediator mediator, ICorrelationContextAccessor correlationContextAccessor)
    {
        _mediator = mediator;
        _correlationContextAccessor = correlationContextAccessor;
    }

    [HttpPost]
    public async Task<CreateReportResponse> Create(CancellationToken cancellationToken)
    {
        var response =
            await _mediator.Send(new CreateReport(_correlationContextAccessor.CorrelationContext.CorrelationId),
                cancellationToken);

        return response;
    }

    [HttpGet("{id:guid}")]
    public async Task<GetReportResponse> Get([FromRoute] GetReport request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }
}