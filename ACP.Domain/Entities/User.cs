using ACP.Domain.Common.Primitives.Auditing;
using ACP.Domain.Shared.User;

namespace ACP.Domain.Entities;

public class User : FullAuditedEntity<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public Gender Gender { get; set; } = Gender.Male;
    
    public int BirthYear { get; set; } = 1960;
    
    public string Address { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public string Avatar { get; set; } = @"default_avatar";
}