using ACP.Domain.Common.Primitives;
using ACP.Domain.Entities.ValueObjects;

namespace ACP.Domain.Entities.Identities;

/// <summary>
/// Currently not used.
/// </summary>
public class IdentityUserRole : Entity<Guid>
{
    public IdentityGuid UserId { get; set; } = null!;

    public IdentityGuid RoleId { get; set; } = null!;
}