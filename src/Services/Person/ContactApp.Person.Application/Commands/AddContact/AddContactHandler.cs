using ContactApp.Person.Domain.Aggregates;
using ContactApp.Person.Domain.Repositories;
using ContactApp.Shared.Abstractions.DDD;
using MediatR;

namespace ContactApp.Person.Application.Commands.AddContact;

public class AddContactHandler : IRequestHandler<AddContact, AddContactResponse>
{
    private readonly IPersonRepository _personRepository;

    public AddContactHandler(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task<AddContactResponse> Handle(AddContact request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.Get(new PersonId(request.PersonId), cancellationToken);

        if (person == null) throw new DomainException("Person not found");

        var contact = GetContact(request);
        person.AddContact(contact);

        await _personRepository.AddContacts(person, cancellationToken);

        return new AddContactResponse
        {
            Id = contact.Id,
            Type = contact.Type,
            Value = contact.Value
        };
    }

    private Contact GetContact(AddContact request)
    {
        return request.Type switch
        {
            ContactTypes.EmailAddress => new Contact(new EmailAddress(request.Value)),
            ContactTypes.PhoneNumber => new Contact(new PhoneNumber(request.Value)),
            ContactTypes.Location => new Contact(new Location(request.Value)),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}