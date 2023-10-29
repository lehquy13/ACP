using ACP.Domain.Common.Primitives;

namespace ACP.Domain.Entities.Identities;

/// <summary>
/// Currently not used.
/// </summary>
public class IdentityUserRole : Entity<Guid>
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }
}