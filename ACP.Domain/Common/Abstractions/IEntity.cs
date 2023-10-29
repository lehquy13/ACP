namespace ACP.Domain.Common.Abstractions;

public interface IEntity<TId> where TId : notnull
{
    TId Id { get; }
}