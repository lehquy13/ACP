using ACP.Domain.Business.Identities;
using ACP.Domain.Business.ValueObjects;
using ACP.Results;

namespace ACP.Domain.DomainServices.Interfaces;

public interface IIdentityDomainServices
{
    /// <summary>
    /// Sign in with email and password
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IdentityUser?> SignInAsync(string email, string password);

    /// <summary>
    /// Create new and empty user with default value: email, password, phoneNumber
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<Result<IdentityUser>> CreateAsync(
        string userName,
        string email,
        string password,
        string phoneNumber);

    Task<Result> ChangePasswordAsync(IdentityGuid identityId, string currentPassword, string newPassword);

    Task<Result> ResetPasswordAsync(string email, string newPassword, string otpCode);
}