using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using ACP.Domain.Common.Abstractions;

namespace ACP.Domain.Interfaces.Repositories;

public interface IReadOnlyRepository<TEntity, TId> where TEntity : class, IEntity<TId> where TId : notnull
{
    //TODO: may include bool includeDetails = false, as a second parameter
    Task<List<TEntity>> GetListAsync( [NotNull] Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(CancellationToken cancellationToken = default);
    
    Task<List<TEntity>> GetPagedListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
}