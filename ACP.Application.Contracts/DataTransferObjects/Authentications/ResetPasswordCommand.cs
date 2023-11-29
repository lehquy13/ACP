namespace ACP.Application.Contracts.DataTransferObjects.Authentications;

public record ResetPasswordCommand
(
    string Email,
    string Otp,
    string NewPassword
);
