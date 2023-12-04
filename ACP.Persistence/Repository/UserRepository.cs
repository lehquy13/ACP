using ACP.Domain.Business;
using ACP.Domain.Business.ValueObjects;
using ACP.Domain.Interfaces;
using ACP.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ACP.Infrastructure.Persistence.Repository;

internal class UserRepository : RepositoryImpl<User, IdentityGuid>, IUserRepository
{
    public UserRepository(AcpDbContext appDbContext, IAppLogger<UserRepository> logger) : base(appDbContext, logger)
    {
    }

    public async Task<User?> GetFullById(IdentityGuid id)
    {
        try
        {
            var fullUser = await AppDbContext
                .Users
                .FirstOrDefaultAsync(x => x.Id == id);
            return fullUser;
        }
        catch (Exception ex)
        {
            Logger.LogError(ErrorMessage, "GetFullById", ex.Message);
            return null;
        }
    }
}