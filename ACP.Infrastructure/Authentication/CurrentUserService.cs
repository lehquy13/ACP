using System.Security.Claims;
using ACP.Application.Contracts.Interfaces.Infrastructures;
using ACP.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace ACP.Infrastructure.Authentication;

public class CurrentUserService : ICurrentUserService, IScoped<ICurrentUserService>
{
    public Guid? CurrentUserId { get; }
    public bool IsAuthenticated { get; }
    public string? CurrentUserEmail { get; }
    public string? CurrentUserFullName { get; }

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userId != null)
        {
            CurrentUserId = new Guid(userId.Value);
            IsAuthenticated = true;
            CurrentUserEmail = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            CurrentUserFullName = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
        }
    }
}