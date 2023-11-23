namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record AuthenticationResult(IdentityUserDto IdentityUserDto, string Token);
