using ACP.Domain.Business.Identities;
using ACP.Domain.Business.ValueObjects;

namespace ACP.Domain.Specifications.Users;

public class UserFindSpec(IdentityGuid id) : FindSpecificationBase<IdentityUser, IdentityGuid>(id);