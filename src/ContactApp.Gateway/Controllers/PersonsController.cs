using ContactApp.Gateway.Services.Person;
using Microsoft.AspNetCore.Mvc;

namespace ContactApp.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonApi _personApi;

    public PersonsController(IPersonApi personApi)
    {
        _personApi = personApi;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePerson request)
    {
        var result = await _personApi.Create(request);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get([FromRoute] GetPerson request)
    {
        var result = await _personApi.Get(request.Id);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetPersons request)
    {
        var result = await _personApi.GetAll(request.Page, request.Size);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeletePerson request)
    {
        var result = await _personApi.Delete(request.Id);

        return Ok(result);
    }

    [HttpPut("{id:guid}/contacts")]
    public async Task<IActionResult> AddContact(Guid id, [FromBody]AddContact request)
    {
        var result = await _personApi.AddContact(id, request);

        return Ok(result);
    }

    [HttpDelete("{personId:guid}/contacts/{id:guid}")]
    public async Task<IActionResult> RemoveContact([FromRoute] RemoveContact request)
    {
        var result = await _personApi.RemoveContact(request.PersonId, request.Id);

        return Ok(result);
    }
}