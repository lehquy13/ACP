namespace ACP.Application.Contracts.Interfaces.Infrastructures;

public interface ICurrentUserService 
{
    Guid? CurrentUserId { get; }
    bool IsAuthenticated { get; }
    string? CurrentUserEmail { get; }
    string? CurrentUserFullName { get; }
}