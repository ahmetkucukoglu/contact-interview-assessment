using ContactApp.Person.Domain.Aggregates;
using MediatR;

namespace ContactApp.Person.Application.Commands.AddContact;

public record AddContact : IRequest<AddContactResponse>
{
    public ContactTypes Type { get; init; }
    public string Value { get; init; }
    public Guid PersonId { get; init; }
}
public record AddContactResponse
{
    public Guid Id { get; init; }
    public ContactTypes Type { get; init; }
    public string Value { get; init; }
}