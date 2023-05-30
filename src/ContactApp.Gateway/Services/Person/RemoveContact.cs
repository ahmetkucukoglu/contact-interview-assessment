namespace ContactApp.Gateway.Services.Person;

public record RemoveContact(Guid Id, Guid PersonId);
public record RemoveContactResponse;