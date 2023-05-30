using ContactApp.Person.Application.Commands.AddContact;
using ContactApp.Person.Application.Commands.CreatePerson;
using ContactApp.Person.Application.Commands.DeletePerson;
using ContactApp.Person.Application.Commands.RemoveContact;
using ContactApp.Person.Application.Queries.GetPerson;
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
    
    [HttpGet("{id:guid}")]
    public async Task<GetPersonResponse> Get([FromRoute] GetPerson request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<DeletePersonResponse> Delete([FromRoute] DeletePerson request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }

    [HttpPut("{id:guid}/contacts")]
    public async Task<AddContactResponse> AddContact(AddContact request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }

    [HttpDelete("{personId:guid}/contacts/{id:guid}")]
    public async Task<RemoveContactResponse> RemoveContact([FromRoute] RemoveContact request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return response;
    }
}