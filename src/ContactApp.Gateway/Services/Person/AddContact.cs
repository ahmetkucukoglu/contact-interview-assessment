namespace ContactApp.Gateway.Services.Person;

public record AddContact
{
    public int Type { get; init; }
    public string Value { get; init; }
}
public record AddContactResponse
{
    public Guid Id { get; init; }
    public int Type { get; init; }
    public string Value { get; init; }
}