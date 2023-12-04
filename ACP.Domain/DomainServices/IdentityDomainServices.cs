using ACP.Domain.Business.Identities;
using ACP.Domain.Business.ValueObjects;
using ACP.Domain.DomainServices.Exceptions;
using ACP.Domain.DomainServices.Interfaces;
using ACP.Domain.Interfaces;
using ACP.Domain.Interfaces.Repositories;
using ACP.Results;

namespace ACP.Domain.DomainServices;

public class IdentityDomainServices(IAppLogger<IdentityDomainServices> logger,
        IIdentityRepository identityUserRepository,
        IRepository<IdentityRole, Guid> identityRoleRepository)
    : DomainServiceBase(logger), IIdentityDomainServices
{
    public async Task<IdentityUser?> SignInAsync(string email, string password)
    {
        await Task.CompletedTask;

        var identityUser = await identityUserRepository
            .FindByEmailAsync(email);

        if (identityUser is null || identityUser.ValidatePassword(password) is false)
        {
            return null;
        }

        return identityUser;
    }

    public async Task<Result<IdentityUser>> CreateAsync(
        string userName,
        string email,
        string password,
        string phoneNumber)
    {
        var roles = await identityRoleRepository
            .GetListAsync();

        if (roles.Count <= 0)
        {
            return Result.Fail(DomainServiceErrors.RoleNotFoundDomainError);
        }

        IdentityUser identityUser = IdentityUser.Create(
            userName,
            email,
            password,
            phoneNumber,
            roles.First().Id
        );

        await identityUserRepository.InsertAsync(identityUser);

        return identityUser;
    }

    public async Task<Result> ChangePasswordAsync(IdentityGuid identityId, string currentPassword, string newPassword)
    {
        var identityUser = await identityUserRepository.FindAsync(identityId);

        if (identityUser is null)
        {
            return Result.Fail(DomainServiceErrors.UserNotFound);
        }

        var verifyResult = identityUser.ValidatePassword(currentPassword);

        if (!verifyResult)
        {
            return Result.Fail(DomainServiceErrors.InvalidPassword);
        }

        identityUser.HandlePassword(newPassword);

        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(string email, string newPassword, string otpCode)
    {
        var identityUser = await identityUserRepository.FindByEmailAsync(email);

        if (identityUser is null)
        {
            return DomainServiceErrors.UserNotFound;
        }

        //Check does otp have same value and still valid
        if (identityUser.OtpCode.Value != otpCode)
        {
            return DomainServiceErrors.InvalidOtp;
        }

        if (identityUser.OtpCode.IsExpired())
        {
            return DomainServiceErrors.OtpExpired;
        }

        identityUser.HandlePassword(newPassword);
        identityUser.OtpCode.Reset();

        return Result.Success();
    }
}