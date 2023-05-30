using ContactApp.Person.Domain.Aggregates;
using ContactApp.Person.Domain.Repositories;
using ContactApp.Shared.Abstractions.DDD;
using MediatR;

namespace ContactApp.Person.Application.Commands.RemoveContact;

public class RemoveContactHandler : IRequestHandler<RemoveContact, RemoveContactResponse>
{
    private readonly IPersonRepository _personRepository;

    public RemoveContactHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<RemoveContactResponse> Handle(RemoveContact request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(new PersonId(request.PersonId), cancellationToken);

        if (person == null) throw new DomainException("Person not found");

        person.RemoveContact(request.Id);

        await _personRepository.RemoveContacts(person, cancellationToken);

        return new RemoveContactResponse();
    }
}