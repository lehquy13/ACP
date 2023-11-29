using ACP.Domain.Business.Identities;

namespace ACP.Domain.Specifications.Identities;

public class IdentityRoleFindSpec(Guid id) : FindSpecificationBase<IdentityRole, Guid>(id)
{
}