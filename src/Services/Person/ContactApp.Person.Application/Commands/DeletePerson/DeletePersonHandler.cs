using ContactApp.Person.Domain.Aggregates;
using ContactApp.Person.Domain.Repositories;
using ContactApp.Shared.Abstractions.DDD;
using MediatR;

namespace ContactApp.Person.Application.Commands.DeletePerson;

public class DeletePersonHandler : IRequestHandler<DeletePerson, DeletePersonResponse>
{
    private readonly IPersonRepository _personRepository;

    public DeletePersonHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<DeletePersonResponse> Handle(DeletePerson request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(new PersonId(request.Id), cancellationToken);

        if (person == null) throw new DomainException("Person not found");

        person.Delete();

        await _personRepository.Delete(person, cancellationToken);

        return new DeletePersonResponse();
    }
}