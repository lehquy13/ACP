using ACP.DependencyInjection;
using ACP.Domain.Business.Identities;
using ACP.Domain.Business.ValueObjects;
using ACP.Domain.Interfaces;
using ACP.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACP.Infrastructure.Persistence.Repository;

internal class IdentityRepository :
    RepositoryImpl<IdentityUser, IdentityGuid>, 
    IIdentityRepository,
    IScoped<IIdentityRepository>
{
    public IdentityRepository(AcpDbContext appDbContext, IAppLogger<IdentityRepository> logger) : base(appDbContext,
        logger)
    {
    }
    

    public async Task<IdentityUser?> GetByIdAsync(IdentityGuid id)
    {
        try
        {
            return await AppDbContext.IdentityUsers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "GetByIdAsync", ex.Message);
            return null;
        }
    }

    public async Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default)
    {
        try
        {
            return await AppDbContext.IdentityUsers
                .Include(x => x.User)
                .Include(x => x.IdentityRole)
                .FirstOrDefaultAsync(x => x.UserName != null && x.UserName.ToUpper() == normalizedUserName, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "FindByNameAsync", ex.Message);
            return null;
        }
    }

    public async Task<IdentityUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default)
    {
        try
        {
            return await AppDbContext.IdentityUsers
                .Include(x => x.User)
                .Include(x => x.IdentityRole)
                .FirstOrDefaultAsync(x => x.Email != null && x.Email.ToUpper() == normalizedEmail, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "FindByEmailAsync", ex.Message);
            return null;
        }
    }

    public async Task<IdentityRole> GetRolesAsync(IdentityGuid userId, CancellationToken cancellationToken = default)
    {
        try
        {
            var identityUser = await AppDbContext.IdentityUsers
                .Include(x => x.IdentityRole)
                .FirstOrDefaultAsync(x => x.Id.Equals(userId), cancellationToken: cancellationToken);

            if (identityUser is null)
            {
                throw new Exception("User not found.");
            }
            
            return identityUser.IdentityRole;

        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "GetRolesAsync", ex.Message);
            return new();
        }
    }
}