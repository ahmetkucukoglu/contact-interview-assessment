using System.Text.RegularExpressions;
using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class PhoneNumber : ValueObject<PhoneNumber>
{
    public string Value { get; private set; }

    public PhoneNumber(string phoneNumber)
    {
        var validateRegex = new Regex("^([0-9]{10})$");

        if (!validateRegex.IsMatch(phoneNumber))
            throw new DomainException("Phone number not valid.");

        Value = phoneNumber;
    }

    protected override bool EqualsCore(PhoneNumber other) => Value == other;

    protected override int GetHashCodeCore() => Value.GetHashCode();
    
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}