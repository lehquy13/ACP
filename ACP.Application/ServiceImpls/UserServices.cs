using ACP.Application.Contracts.DataTransferObjects.Users;
using ACP.Application.Contracts.Interfaces.Business;
using ACP.DependencyInjection;
using ACP.Domain.Business;
using ACP.Domain.Business.Identities;
using ACP.Domain.Business.ValueObjects;
using ACP.Domain.DomainServices.Interfaces;
using ACP.Domain.Interfaces;
using ACP.Domain.Shared.Paginated;
using ACP.Domain.Specifications.Users;
using ACP.Results;
using MapsterMapper;

namespace ACP.Application.ServiceImpls;

public class UserServices : ServiceBase, IUserServices, IScoped<IUserServices>
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityRepository _identityRepository;
    private readonly IIdentityDomainServices _identityDomainServices;

    public UserServices(
        IMapper mapper,
        IAppLogger<UserServices> logger,
        IUnitOfWork unitOfWork,
        IUserRepository userRepository, IIdentityDomainServices identityDomainServices,
        IIdentityRepository identityRepository)
        : base(mapper, unitOfWork, logger)
    {
        _userRepository = userRepository;
        _identityDomainServices = identityDomainServices;
        _identityRepository = identityRepository;
    }

    public async Task<PaginationResult<UserForListDto>> GetUsers(PaginatedParams userFilterParams)
    {
        try
        {
            var userSpec = new UserListQuerySpec(userFilterParams.PageIndex, userFilterParams.PageSize);
            var totalCount = await _userRepository.CountAsync(userSpec);
            var users = await _userRepository.GetListAsync(userSpec);
            var usersForListDto = Mapper.Map<List<UserForListDto>>(users);

            return PaginationResult<UserForListDto>
                .Create(
                    usersForListDto,
                    totalCount,
                    userFilterParams.PageIndex,
                    userFilterParams.PageSize);
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return e;
        }
    }

    public async Task<Result<UserForDetailDto>> GetUserDetailByIdAsync(Guid id)
    {
        try
        {
            var user = await _identityRepository.GetAsync(
                new UserFindSpec(IdentityGuid.Create(id)));

            if (user is null)
            {
                Logger.LogError("{Message} with Id {id}", UserErrorMessages.UserNotFound.Description, id);
                return Result.Fail(UserErrorMessages.UserNotFound);
            }

            var userForDetailDto = Mapper.Map<UserForDetailDto>(user.User);

            return userForDetailDto;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return e;
        }
    }

    public async Task<Result<UserForBasicDto>> GetUserBasicByIdAsync(Guid id)
    {
        try
        {
            var user = await _identityRepository.GetAsync(
                new UserFindSpec(IdentityGuid.Create(id))
            );

            if (user is null)
            {
                Logger.LogError("{Message} with Id {id}", UserErrorMessages.UserNotFound, id);
                return Result.Fail(UserErrorMessages.UserNotFound);
            }

            var userForDetailDto = Mapper.Map<UserForBasicDto>(user);

            return userForDetailDto;
        }
        catch (Exception e)
        {
            Logger.LogError(e.Message);
            return e;
        }
    }

    public async Task<Result> UpsertUserAsync(UserForUpsertDto userForUpsertDto)
    {
        var user = await _userRepository.FindAsync(IdentityGuid.Create(userForUpsertDto.Id));

        if (user is null)
        {
            //Create new user
            user = Mapper.Map<User>(userForUpsertDto);

            await _userRepository.InsertAsync(user);
        }
        else
        {
            //Update user
            Mapper.Map(userForUpsertDto, user);
        }

        if (await UnitOfWork.SaveChangesAsync() <= 0)
        {
            Logger.LogError(UserErrorMessages.UpsertFail.Description);
            return UserErrorMessages.UpsertFail;
        }

        return Result.Success();
    }

    public async Task<Result> DeleteUserAsync(Guid id)
    {
        await _userRepository.DeleteAsync(IdentityGuid.Create(id));

        if (await UnitOfWork.SaveChangesAsync() <= 0)
        {
            Logger.LogError("{Message}", UserErrorMessages.DeleteFailWithException);
            return Result.Fail(UserErrorMessages.DeleteFailWithException);
        }

        return Result.Success();
    }
}