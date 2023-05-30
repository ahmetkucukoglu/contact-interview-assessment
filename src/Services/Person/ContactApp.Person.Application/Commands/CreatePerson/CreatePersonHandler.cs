using ContactApp.Person.Domain.Aggregates;
using ContactApp.Person.Domain.Repositories;
using MediatR;

namespace ContactApp.Person.Application.Commands.CreatePerson;

public class CreatePersonHandler : IRequestHandler<CreatePerson, CreatePersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public CreatePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<CreatePersonResponse> Handle(CreatePerson request, CancellationToken cancellationToken)
    {
        var person = new Domain.Aggregates.Person(
            request.FirstName, request.LastName, new CompanyId(request.CompanyId));

        await _personRepository.Create(person, cancellationToken);

        return new CreatePersonResponse
        {
            Id = person.Id.Value,
            FirstName = person.FirstName,
            LastName = person.LastName,
            CompanyId = person.CompanyId.Value
        };
    }
}