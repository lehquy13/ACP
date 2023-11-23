using ACP.Domain.Entities.ValueObjects;
using ACP.Domain.Interfaces.Repositories;

namespace ACP.Domain.Entities;

public interface IUserRepository : IRepository<User, IdentityGuid>
{
    
}