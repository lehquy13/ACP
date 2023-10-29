namespace ACP.Domain.Common.Abstractions;

public interface IAggregateRoot<TId> : IEntity<TId> where TId : notnull
{
}