namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record RegisterCommand
(
    string Name,
    string Email,
    string Password,
    string City,
    string Country,
    string PhoneNumber
);
