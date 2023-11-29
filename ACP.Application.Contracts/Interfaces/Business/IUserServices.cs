using ACP.Application.Contracts.DataTransferObjects.Users;
using ACP.Domain.Shared.Paginated;
using ACP.Results;

namespace ACP.Application.Contracts.Interfaces.Business;

public interface IUserServices
{
    Task<PaginationResult<UserForListDto>> GetUsers(PaginatedParams userFilterParams);
    
    Task<Result<UserForDetailDto>> GetUserDetailByIdAsync(Guid id);
    
    Task<Result<UserForBasicDto>> GetUserBasicByIdAsync(Guid id);
    
    Task<Result> UpsertUserAsync(UserForUpsertDto userForUpsertDto);
    
    Task<Result> DeleteUserAsync(Guid id);
}