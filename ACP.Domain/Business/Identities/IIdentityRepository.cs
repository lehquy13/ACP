using ACP.Domain.Business.ValueObjects;
using ACP.Domain.Interfaces.Repositories;

namespace ACP.Domain.Business.Identities;

public interface IIdentityRepository : IRepository<IdentityUser, IdentityGuid>
{
    Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default);
    Task<IdentityUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);

    Task<IdentityRole> GetRolesAsync(IdentityGuid userId, CancellationToken cancellationToken = default);
}