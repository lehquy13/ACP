namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record RegisterCommand
(
    string Username,
    string Email,
    string Password,
    string PhoneNumber
);
