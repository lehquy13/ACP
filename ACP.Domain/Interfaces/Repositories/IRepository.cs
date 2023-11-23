using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using ACP.Domain.Common.Abstractions;

namespace ACP.Domain.Interfaces.Repositories;

//TODO: may change IEntity<TEntity> to IAggregateRoot<TId> 
public interface IRepository<TEntity, TId> : IReadOnlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId> where TId : notnull
{
    //Queries
    /// <summary>
    /// Get all the record of tables into a list of object
    /// </summary>
    Task<List<TEntity>> GetAllListAsync();

    /// <summary>
    /// Get all the record of tables and able to query with linq due to the iqueryable<> return
    /// </summary>
    IQueryable<TEntity> GetAll();

    Task<TEntity?> FindAsync([NotNull] Expression<Func<TEntity, bool>> predicate);

    //Insert
    Task<TEntity> Insert(TEntity entity);

    //Remove
    Task DeleteAsync([NotNull] Expression<Func<TEntity, bool>> predicate);
}