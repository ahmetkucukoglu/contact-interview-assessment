using MediatR;

namespace ContactApp.Person.Application.Commands.RemoveContact;

public record RemoveContact(Guid Id, Guid PersonId) : IRequest<RemoveContactResponse>;
public record RemoveContactResponse;