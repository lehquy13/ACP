using ACP.Mediator.Abstraction;

namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record LoginQuery(string Email, string Password) : IRequest<AuthenticationResult>;