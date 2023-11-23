using ACP.Results;

namespace ACP.Domain.Errors;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string userName) : base($"No user found with username: {userName}")
    {
    }
}

//Test new User Error using ACP.Results
//Compare this snippet from ACP.Domain/Errors/UserNotFoundException.cs:
public static class UserNotFoundError
{
    public static Error UserNotFound() =>
        new("UserNotFound",
            "No user found");
    public static Error UserNotFound(string userName) =>
        new("UserNotFound",
            $"No user found with username: {userName}");
}