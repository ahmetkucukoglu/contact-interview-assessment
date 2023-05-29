namespace ContactApp.Shared.Abstractions.DDD;

public interface IDeleteEntity
{
    bool IsDeleted { get; set; }
}