using MediatR;

namespace ContactApp.Person.Application.Commands.DeletePerson;

public record DeletePerson(Guid Id) : IRequest<DeletePersonResponse>;
public record DeletePersonResponse;