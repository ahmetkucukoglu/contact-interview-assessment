using System.Text.RegularExpressions;
using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class Location : ValueObject<Location>
{
    public string Value { get; private set; }

    public Location(string location)
    {
        var validateRegex = new Regex("^[a-zA-Z0-9ğüşöçİĞÜŞÖÇ ]+$");

        if (!validateRegex.IsMatch(location))
            throw new DomainException("Location not valid.");

        Value = location;
    }

    protected override bool EqualsCore(Location other) => Value == other;

    protected override int GetHashCodeCore() => Value.GetHashCode();
    
    public static implicit operator string(Location location) => location.Value;
}