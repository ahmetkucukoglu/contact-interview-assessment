using ContactApp.Person.Domain.Aggregates;
using MediatR;

namespace ContactApp.Person.Application.Queries.GetPerson;

public record GetPerson(Guid Id) : IRequest<GetPersonResponse>;

public record GetPersonResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Guid CompanyId { get; init; }
    public string CompanyName { get; init; }
    public IEnumerable<GetPersonContact> Contacts { get; init; }
}

public record GetPersonContact()
{
    public Guid Id { get; init; }
    public ContactTypes Type { get; init; }
    public string Value { get; init; }
}