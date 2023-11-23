namespace ACP.Domain.Entities.Identities;

public interface IUserManager
{
    Task<IdentityUser> FindByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IdentityUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default);

    Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default);
    
    Task<IdentityUser> GetRolesAsync(Guid userId, CancellationToken cancellationToken = default);
}