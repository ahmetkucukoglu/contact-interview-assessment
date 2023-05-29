namespace ContactApp.Shared.Abstractions.DDD;

public interface IAuditEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset ModifiedAt { get; set; }
}