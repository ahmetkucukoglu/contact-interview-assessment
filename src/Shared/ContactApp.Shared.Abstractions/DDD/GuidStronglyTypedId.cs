namespace ContactApp.Shared.Abstractions.DDD;

public abstract class GuidStronglyTypedId<T> : ValueObject<GuidStronglyTypedId<T>>
{
    public Guid Value { get; }

    protected GuidStronglyTypedId(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("A valid id must be provided.");

        Value = value;
    }

    protected override bool EqualsCore(GuidStronglyTypedId<T> other)
    {
        return Value == other.Value;
    }

    protected override int GetHashCodeCore()
    {
        unchecked
        {
            return Value.GetHashCode();
        }
    }
}