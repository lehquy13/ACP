using ACP.Domain.Common.Primitives;

namespace ACP.Domain.Entities.Identities;

public class IdentityUser : AggregateRoot<Guid>
{
    public IdentityUser()
    {
    }

    public IdentityUser(string userName) : this()
    {
        UserName = userName;
    }

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }
    
    public Guid IdentityRoleId { get; set; }
    
    public IdentityRole IdentityRole { get; set; }

    //public virtual DateTimeOffset? LockoutEnd { get; set; }

    //public virtual bool LockoutEnabled { get; set; }

    //public virtual int AccessFailedCount { get; set; }

    public override string ToString()
        => UserName ?? string.Empty;
}