namespace ContactApp.Shared.HttpServices.Person;

public record RemoveContact(Guid Id, Guid PersonId);
public record RemoveContactResponse;