using ACP.Domain.Common.Abstractions;

namespace ACP.Domain.Interfaces.Repositories;

public interface IRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot<TId> where TId : notnull
{
    /// <summary>
    /// Get all the record of tables and able to query with linq due to the Queryable return
    /// Consider to remove this method
    /// </summary>
    IQueryable<TEntity> GetAll();

    Task<TEntity?> FindAsync(TId id);

    //Insert
    Task<TEntity> InsertAsync(TEntity entity);

    //Remove
    Task DeleteAsync(TId spec);
}