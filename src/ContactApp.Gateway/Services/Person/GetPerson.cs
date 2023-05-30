namespace ContactApp.Gateway.Services.Person;

public record GetPerson(Guid Id);

public record GetPersonResponse
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public Guid CompanyId { get; init; }
    public IEnumerable<GetPersonContact> Contacts { get; init; }
}

public record GetPersonContact
{
    public Guid Id { get; init; }
    public int Type { get; init; }
    public string Value { get; init; }
}