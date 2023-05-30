using ContactApp.Company.Application.Commands.CreateCompany;
using ContactApp.Company.Application.Queries.GetCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Company.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompaniesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateCompanyResponse> Create(CreateCompany request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }

    [HttpGet("{id:guid}")]
    public async Task<GetCompanyResponse> Get([FromRoute]GetCompany request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }
}