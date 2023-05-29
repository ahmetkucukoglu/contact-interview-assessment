namespace ContactApp.Shared.Abstractions.DDD;

public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregateRoot, IDeleteEntity, IAuditEntity
{
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public DateTimeOffset ModifiedAt { get; set; } = DateTimeOffset.Now;
}