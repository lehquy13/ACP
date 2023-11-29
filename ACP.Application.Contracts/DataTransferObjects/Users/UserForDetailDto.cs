using ACP.Application.Contracts.Abstractions.Primitives;

namespace ACP.Application.Contracts.DataTransferObjects.Users;

/// <summary>
/// May update FullAuditedEntityDto later
/// </summary>
public class UserForDetailDto : EntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public decimal BalanceAmount { get; set; } = 0;
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        return $"User with Id: {Id}" +
               $"\nName: {Name}" +
               $"\nBalance: {BalanceAmount}" +
               $"\nEmail: {Email}" +
               $"\nPhoneNumber: {PhoneNumber}" +
               $"\nAddress: {Address}";
    }
}