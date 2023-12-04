using ACP.DependencyInjection;
using ACP.Domain.Common.Abstractions;
using ACP.Domain.Common.Primitives;
using ACP.Domain.Interfaces;
using ACP.Domain.Interfaces.Repositories;
using ACP.Domain.Specifications.Interfaces;
using ACP.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACP.Infrastructure.Persistence.Repository;

internal class ReadonlyRepositoryImpl<TEntity, TId> : IReadOnlyRepository<TEntity, TId>, IScoped<IRepository<TEntity, TId>>
    where TEntity : Entity<TId>, IAggregateRoot<TId>
    where TId : notnull
{
    protected readonly AcpDbContext AppDbContext;
    protected readonly IAppLogger<RepositoryImpl<TEntity, TId>> Logger;
    protected readonly string ErrorMessage = "{Message} with exception: {Ex}";

    public ReadonlyRepositoryImpl(
        AcpDbContext appDbContext, 
        IAppLogger<RepositoryImpl<TEntity, TId>> logger)
    {
        AppDbContext = appDbContext;
        this.Logger = logger;
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await AppDbContext.Set<TEntity>().CountAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "GetAllListAsync", ex.Message);
            return 0;
        }
    }

    public async Task<long> CountAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var specificationResult = GetQuery(AppDbContext.Set<TEntity>(), spec, true);

        return await specificationResult.CountAsync(cancellationToken: cancellationToken);
    }

    public IQueryable<TEntity> GetAll()
    {
        try
        {
            return AppDbContext.Set<TEntity>().AsQueryable<TEntity>();
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "Error in GetAll", ex.Message);
            throw;
        }
    }

    protected static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification, bool isForCount = false)

    {
        var query = inputQuery;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));

        //Handle then include
        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.IsPagingEnabled && !isForCount)
        {
            query = query.Skip(specification.Skip - 1)
                .Take(specification.Take);
        }

        return query;
    }

    public async Task<TEntity?> GetAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken = default)
    {
        var specificationResult = GetQuery(AppDbContext.Set<TEntity>(), spec);

        return await specificationResult.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await AppDbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "Error in GetListAsync", ex.Message);
            return new();
        }
    }

    public async Task<List<TEntity>> GetListAsync(IGetListSpecification<TEntity> spec,
        CancellationToken cancellationToken = default)
    {
        var specificationResult = GetQuery(AppDbContext.Set<TEntity>(), spec);

        return await specificationResult.AsNoTracking().ToListAsync(cancellationToken: cancellationToken);
    }
}