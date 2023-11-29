namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record ChangePasswordCommand
(
    string Id,
    string CurrentPassword,
    string NewPassword
);