using ACP.Application.Contracts.DataTransferObjects.Authentications;

namespace ACP.Application.Contracts.Interfaces.Infrastructures;

public interface IJwtTokenGenerator
{
    string GenerateToken(IdentityUserDto identityUserDto);
    bool ValidateToken(string token);
}