namespace ContactApp.Shared.Abstractions.DDD;

public abstract class StringStronglyTypedId<T> : ValueObject<StringStronglyTypedId<T>>
{
    public string Value { get; }

    protected StringStronglyTypedId(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new DomainException("A valid id must be provided.");

        Value = value;
    }

    protected override bool EqualsCore(StringStronglyTypedId<T> other)
    {
        return Value == other.Value;
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }
}