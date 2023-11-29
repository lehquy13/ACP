using ACP.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACP.Infrastructure.AppLogger;

public class AppLogger<TCategory>(ILogger<TCategory> logger) : IAppLogger<TCategory>
    where TCategory : class
{
    public void LogInformation(string message, params object[] args)
    {
        logger.LogInformation("{Message}", args);
    }

    public void LogWarning(string message, params object[] args)
    {
        logger.LogWarning("{Message}", args);
    }

    public void LogError(string message, params object[] args)
    {
        logger.LogError("{Message}", args);
    }
}