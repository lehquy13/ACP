using ACP.Domain.Repositories;

namespace ACP.Domain.Entities;

public interface IUserRepository : IRepository<User, Guid>
{
    
}