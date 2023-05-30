using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class Contact : Entity<Guid>
{
    public ContactTypes Type { get; private set; }
    public string Value { get; private set; }

    public Contact(EmailAddress emailAddress)
    {
        Id = Guid.NewGuid();
        Type = ContactTypes.EmailAddress;
        Value = emailAddress;
    }

    public Contact(PhoneNumber phoneNumber)
    {
        Id = Guid.NewGuid();
        Type = ContactTypes.PhoneNumber;
        Value = phoneNumber;
    }

    public Contact(Location location)
    {
        Id = Guid.NewGuid();
        Type = ContactTypes.Location;
        Value = location;
    }
}