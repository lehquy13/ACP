using ACP.Application.Contracts.DataTransferObjects.Authentications;
using ACP.Application.Contracts.Interfaces;
using ACP.Application.Contracts.Interfaces.Business;
using ACP.Application.Contracts.Interfaces.Infrastructures;
using ACP.Domain.Business.Identities;
using ACP.Domain.Business.ValueObjects;
using ACP.Domain.DomainServices.Interfaces;
using ACP.Domain.Interfaces;
using ACP.Domain.Specifications.Interfaces;
using ACP.Domain.Specifications.Users;
using ACP.Results;
using MapsterMapper;

namespace ACP.Application.ServiceImpls;

public class AuthenticationServices(IMapper mapper,
        IUnitOfWork unitOfWork,
        IAppLogger<ServiceBase> logger,
        IIdentityRepository identityRepository,
        IIdentityDomainServices identityDomainServices,
        IJwtTokenGenerator jwtTokenGenerator,
        IEmailSender emailSender)
    : ServiceBase(mapper, unitOfWork, logger), IAuthenticationServices
{
    public async Task<Result<AuthenticationResult>> Login(LoginQuery loginQuery)
    {
        try
        {
            var identityUser = await identityDomainServices.SignInAsync(loginQuery.Email, loginQuery.Password);

            if (identityUser is null)
            {
                return Result.Fail(AuthenticationErrorMessages.LoginFailError);
            }

            var userLoginDto = Mapper.Map<IdentityUserDto>(identityUser);

            var token = jwtTokenGenerator.GenerateToken(userLoginDto);

            return new AuthenticationResult(userLoginDto, token);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);

            return Result.Fail(AuthenticationErrorMessages.LoginFailError);
        }
    }

    public async Task<Result> ResetPassword(ResetPasswordCommand resetPasswordCommand)
    {
        var result = await identityDomainServices
            .ResetPasswordAsync(
                resetPasswordCommand.Email,
                resetPasswordCommand.NewPassword,
                resetPasswordCommand.Otp);

        if (!result.IsSuccess)
        {
            return Result.Fail(AuthenticationErrorMessages.ResetPasswordFail)
                .WithError(result.Error);
        }

        if (await UnitOfWork.SaveChangesAsync() <= 0)
        {
            return AuthenticationErrorMessages.ResetPasswordFailWhileSavingChanges;
        }

        return Result.Success();
    }

    public async Task<Result> ChangePassword(ChangePasswordCommand changePasswordCommand)
    {
        var identityId = IdentityGuid.Create(new Guid(changePasswordCommand.Id));
        var result = await identityDomainServices
            .ChangePasswordAsync(
                identityId,
                changePasswordCommand.CurrentPassword,
                changePasswordCommand.NewPassword);

        if (!result.IsSuccess)
        {
            var resultToReturn = Result.Fail(AuthenticationErrorMessages.ChangePasswordFail)
                .WithError(result.Error);
            return resultToReturn;
        }

        if (await UnitOfWork.SaveChangesAsync() <= 0)
        {
            return Result.Fail(AuthenticationErrorMessages.ChangePasswordFailWhileSavingChanges);
        }

        return Result.Success();
    }

    public async Task<Result> ForgotPassword(string email)
    {
        var identityUser = await identityRepository.GetAsync(
               new UserGetByEmailSpec(email)
            );

        ISpecification<IdentityUser> spec = new UserGetByEmailSpec(email);
        if (identityUser is null)
        {
            return Result.Fail(AuthenticationErrorMessages.UserNotFound);
        }

        identityUser.GenerateOtpCode();

        if (await UnitOfWork.SaveChangesAsync() <= 0)
        {
            return Result.Fail(AuthenticationErrorMessages.ResetPasswordFail);
        }

        var message = $"Your OTP code is {identityUser.OtpCode.Value}";

#pragma warning disable
        emailSender.SendEmail(email, "Reset password", message);
#pragma warning restore

        return Result.Success();
    }

    public async Task<Result<AuthenticationResult>> Register(RegisterCommand registerCommand)
    {
        try
        {
            var result = await identityDomainServices.CreateAsync(
                registerCommand.Username,
                registerCommand.Email,
                registerCommand.Password,
                registerCommand.PhoneNumber
            );

            if (!result.IsSuccess)
            {
                var resultToReturn = Result.Fail(AuthenticationErrorMessages.RegisterFail)
                    .WithError(result.Error);
                return resultToReturn;
            }

            if (await UnitOfWork.SaveChangesAsync() < 0)
            {
                return Result.Fail(AuthenticationErrorMessages.CreateUserFailWhileSavingChanges);
            }

            var userLoginDto = Mapper.Map<IdentityUserDto>(result.Value!);

            var token = jwtTokenGenerator.GenerateToken(userLoginDto);

            return new AuthenticationResult(userLoginDto, token);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);

            return Result.Fail(AuthenticationErrorMessages.RegisterFail)
                .WithError(e);
        }
    }

    public async Task<Result> ValidateToken(ValidateTokenQuery validateTokenQuery)
    {
        await Task.CompletedTask;
        if (jwtTokenGenerator.ValidateToken(validateTokenQuery.ValidateToken))
        {
            return Result.Success();
        }

        return Result.Fail(AuthenticationErrorMessages.InvalidToken);
    }

    public AuthenticationServices(IMapper mapper,
        IUnitOfWork unitOfWork,
        IAppLogger<ServiceBase> logger,
        IIdentityDomainServices identityDomainServices,
        IJwtTokenGenerator jwtTokenGenerator,
        IEmailSender emailSender) : this(mapper, unitOfWork, logger, null, identityDomainServices, jwtTokenGenerator, emailSender)
    {
    }
}
