using ACP.Application.Contracts.Abstractions.Primitives;

namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public class IdentityUserDto : EntityDto<Guid>
{
    //User information
    public string Name { get; set; } = string.Empty;

    //Account References
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}