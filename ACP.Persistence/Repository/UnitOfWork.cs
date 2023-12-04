using ACP.Application.Contracts.Interfaces.Infrastructures;
using ACP.Auditing.Abstractions;
using ACP.Domain.Interfaces;
using ACP.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace ACP.Infrastructure.Persistence.Repository;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly AcpDbContext _appDbContext;

    public UnitOfWork(ILogger<UnitOfWork> logger, AcpDbContext appDbContext, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _appDbContext = appDbContext;
        _currentUserService = currentUserService;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        _logger.LogDebug("On save changes...");
        return await _appDbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IHasCreationTime>> hasCreationTimeEntries =
            _appDbContext.ChangeTracker.Entries<IHasCreationTime>();

        foreach (var entityEntry in hasCreationTimeEntries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(e => e.CreationTime).CurrentValue = DateTime.Now;

                // If the entity is type of ICreationAuditedObject<T>, we should set CreatorId
                //TODO: The logic here may goes wrong
                if (entityEntry.Entity is ICreationAuditedObject<object>)
                {
                    entityEntry.Property("CreatorId").CurrentValue = _currentUserService.CurrentUserId;
                }
            }
        }

        IEnumerable<EntityEntry<IHasModificationTime>> hasModificationTimeEnties =
            _appDbContext.ChangeTracker.Entries<IHasModificationTime>();

        foreach (var entityEntry in hasModificationTimeEnties)
        {
            if (entityEntry.State == EntityState.Added || entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(e => e.LastModificationTime).CurrentValue = DateTime.Now;
                
                //TODO: The logic here may goes wrong
                if (entityEntry.Entity is IModificationAuditedObject<object>)
                {
                    entityEntry.Property("LastModifierId").CurrentValue = _currentUserService.CurrentUserId;
                }
            }
        }
    }
}