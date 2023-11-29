using ACP.Domain.Business.ValueObjects;
using ACP.Domain.Common.Primitives.Auditing;
using ACP.Domain.Shared.User;

namespace ACP.Domain.Business;

public class User : FullAuditedAggregateRoot<IdentityGuid>
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public Gender Gender { get; private set; } = Gender.Male;

    public int BirthYear { get; private set; } = 1960;

    public Address Address { get; private set; } = new();

    public string Description { get; private set; } = string.Empty;

    public string Avatar { get; private set; } = @"default_avatar";

    internal User()
    {
    }

    internal static User Create(
        string firstName,
        string lastName,
        Gender gender,
        int birthYear,
        Address address,
        string description,
        string avatar
    )
    {
        return new User
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            BirthYear = birthYear,
            Address = address,
            Description = description,
            Avatar = avatar
        };
    }
}