using ACP.Domain.Common.Abstractions;
using ACP.Domain.Specifications.Interfaces;

namespace ACP.Domain.Interfaces.Repositories;

//TODO: Consider to change IEntity to IAggregateRoot
public interface IReadOnlyRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TId : notnull
{
    Task<TEntity?> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(IGetListSpecification<TEntity> spec, CancellationToken cancellationToken = default);

    Task<long> CountAsync(CancellationToken cancellationToken = default);

    Task<long> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default);
}