using ContactApp.Person.Application.Commands.CreatePerson;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Person.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PersonsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreatePersonResponse> Create(CreatePerson request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }
}