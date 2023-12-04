using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ACP.Infrastructure.Authentication;

public static class AuthorizeExtensions 
{
    public static Guid GetAuthorizedUserId(this ControllerBase controllerBase)
    {
        var userId = controllerBase.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userId == null)
            throw new UnauthorizedAccessException("User is not authorized");

        return new Guid(userId.Value);
    }
}