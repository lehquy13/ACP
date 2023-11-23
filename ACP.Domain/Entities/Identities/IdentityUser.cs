using System.Security.Cryptography;
using System.Text;
using ACP.Domain.Common.Primitives;
using ACP.Domain.Entities.ValueObjects;

namespace ACP.Domain.Entities.Identities;

public class IdentityUser : AggregateRoot<IdentityGuid>
{
    public IdentityUser Create(
        string? userName,
        string? email,
        string? password,
        IdentityGuid identityUserId,
        Guid identityRoleId)
    {
        SetUserName(userName);
        SetEmail(email);
        HandlePassword(password);
        
        IdentityRoleId = identityRoleId;
        UserId = identityUserId;

        return this;
    }

    public string? UserName { get; private set; }

    public string? NormalizedUserName { get; private set; }

    public string? Email { get; private set; }

    public string? NormalizedEmail { get; private set; }

    public bool EmailConfirmed { get; private set; }

    public byte[]? PasswordHash { get; private set; }

    public byte[]? PasswordSalt { get; private set; }

    public string? ConcurrencyStamp { get; private set; } = Guid.NewGuid().ToString();

    public string? PhoneNumber { get; private set; }

    public bool PhoneNumberConfirmed { get; private set; }

    public OtpCode OtpCode { get; private set; } = new();

    public Guid IdentityRoleId { get; private set; }

    public IdentityRole IdentityRole { get; private set; } = null!;

    public User User { get; private set; } = null!;

    public IdentityGuid UserId { get; private set; } = null!;

    //public virtual DateTimeOffprivate set? LockoutEnd { get; private set; }

    //public virtual bool LockoutEnabled { get; private set; }

    //public virtual int AccessFailedCount { get; private set; }

    internal void SetUserName(string? value)
    {
        if (UserName is not null && value is null)
        {
            throw new ArgumentException("Invalid user name.");
        }

        if (value is not null)
        {
            NormalizedUserName = value.ToUpperInvariant();
        }

        UserName = value;
    }

    internal void SetEmail(string? value)
    {
        if (Email is not null && value is null)
        {
            throw new ArgumentException("Invalid email.");
        }
        
        if (value is not null)
        {
            NormalizedEmail = value.ToUpperInvariant();
        }

        Email = value;
    }
    
    private void HandlePassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Invalid password.");
        }

        using var hmac = new HMACSHA512();

        PasswordSalt = hmac.Key;
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public override string ToString()
        => UserName ?? string.Empty;
}