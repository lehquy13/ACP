using ACP.Domain.Entities;

namespace ACP.Domain.Interfaces;

public interface IUserManager
{
    Task<User?> FindByNameAsync(string userName);
    string? GetRolesAsync(User user);
}
