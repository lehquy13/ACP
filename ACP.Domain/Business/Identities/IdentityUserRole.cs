using ACP.Domain.Business.ValueObjects;
using ACP.Domain.Common.Primitives;

namespace ACP.Domain.Business.Identities;

/// <summary>
/// Currently not used.
/// </summary>
public class IdentityUserRole : Entity<Guid>
{
    public IdentityGuid UserId { get; set; } = null!;

    public IdentityGuid RoleId { get; set; } = null!;
}