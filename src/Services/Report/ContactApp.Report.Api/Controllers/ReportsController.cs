using ContactApp.Report.Application.Commands.CreateReport;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Report.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateReportResponse> Create(CreateReport request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }
}