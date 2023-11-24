using ACP.Domain.Common.Primitives.Auditing;
using ACP.Domain.Entities.ValueObjects;
using ACP.Domain.Shared.User;

namespace ACP.Domain.Entities;

public class User : FullAuditedAggregateRoot<IdentityGuid>
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public Gender Gender { get; private set; } = Gender.Male;

    public int BirthYear { get; private set; } = 1960;

    public Address Address { get; private set; } = new();

    public string Description { get; private set; } = string.Empty;

    public string Avatar { get; private set; } = @"default_avatar";
}