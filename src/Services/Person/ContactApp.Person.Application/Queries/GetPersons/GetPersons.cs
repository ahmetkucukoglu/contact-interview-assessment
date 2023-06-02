using MediatR;

namespace ContactApp.Person.Application.Queries.GetPersons;

public record GetPersons(int Page = 1, int Size = 10) : IRequest<GetPersonsResponse>;

public record GetPersonsResponse(int Count, IEnumerable<GetPersonsData> Persons);

public record GetPersonsData
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
}