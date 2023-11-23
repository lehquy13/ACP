using ACP.Application.Contracts.DataTransferObjects.Authentications;

namespace ACP.Application.Contracts.Interfaces;

public interface ITokenClaimsService
{
    string GetTokenAsync(IdentityUserDto identityUserDto);
}