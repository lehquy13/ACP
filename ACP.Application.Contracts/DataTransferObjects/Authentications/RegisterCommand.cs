using ACP.Mediator.Abstraction;

namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record RegisterCommand
(
    string Username,
    string Email,
    string Password,
    string PhoneNumber
) : IRequest<AuthenticationResult>;
