using ACP.Domain.Common.Abstractions;
using ACP.Domain.Common.Primitives;
using ACP.Domain.Interfaces;
using ACP.Domain.Interfaces.Repositories;
using ACP.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACP.Infrastructure.Persistence.Repository;

internal class RepositoryImpl<TEntity, TId> : ReadonlyRepositoryImpl<TEntity, TId>, IRepository<TEntity, TId>
    where TEntity : Entity<TId>, IAggregateRoot<TId>
    where TId : notnull
{
    public RepositoryImpl(AcpDbContext appDbContext,
        IAppLogger<RepositoryImpl<TEntity, TId>> logger)
        : base(appDbContext, logger)
    {
    }

    public async Task InsertAsync(TEntity entity)
    {
        try
        {
            await AppDbContext.Set<TEntity>().AddAsync(entity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "Error in InsertAsync", ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(TId id)
    {
        try
        {
            var deleteRecord = await AppDbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (deleteRecord == null)
            {
                return false;
            }

            AppDbContext.Set<TEntity>().Remove(deleteRecord);
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "GetAllListAsync", ex.Message);
            return new();
        }
    }

    public async Task<TEntity?> FindAsync(TId id)
    {
        try
        {
            return await AppDbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "Error in FindAsync", ex.Message);
            return null;
        }
    }
}