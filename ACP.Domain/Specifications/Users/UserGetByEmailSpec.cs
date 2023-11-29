using ACP.Domain.Business.Identities;

namespace ACP.Domain.Specifications.Users;

public class UserGetByEmailSpec : SpecificationBase<IdentityUser>, Interfaces.ISpecification<IdentityUser>
{
    public UserGetByEmailSpec(string email)
    {
        Criteria = user => user.Email == email;    
    }
}