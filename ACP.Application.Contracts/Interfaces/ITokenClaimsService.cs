namespace ACP.Application.Contracts.Interfaces;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}