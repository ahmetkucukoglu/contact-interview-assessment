using MediatR;

namespace ContactApp.Person.Application.Commands.CreatePerson;

public record CreatePerson : IRequest<CreatePersonResponse>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Guid CompanyId { get; init; }
}

public record CreatePersonResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Guid CompanyId { get; init; }
}