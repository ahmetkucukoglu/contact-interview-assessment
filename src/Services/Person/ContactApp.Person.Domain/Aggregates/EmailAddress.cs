using System.Text.RegularExpressions;
using ContactApp.Shared.Abstractions.DDD;

namespace ContactApp.Person.Domain.Aggregates;

public class EmailAddress : ValueObject<EmailAddress>
{
    public string Value { get; private set; }

    public EmailAddress(string emailAddress)
    {
        var validateRegex = new Regex("^\\S+@\\S+\\.\\S+$");

        if (!validateRegex.IsMatch(emailAddress))
            throw new DomainException("Email address not valid.");

        Value = emailAddress;
    }

    protected override bool EqualsCore(EmailAddress other) => Value == other;

    protected override int GetHashCodeCore() => Value.GetHashCode();

    public static implicit operator string(EmailAddress emailAddress) => emailAddress.Value;
}