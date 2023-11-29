using ACP.Domain.Business.ValueObjects;
using ACP.Domain.Interfaces.Repositories;

namespace ACP.Domain.Business;

public interface IUserRepository : IRepository<User, IdentityGuid>
{
    
}