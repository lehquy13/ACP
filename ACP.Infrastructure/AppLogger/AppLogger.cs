using ACP.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ACP.Infrastructure.AppLogger;

public class AppLogger<TCategory>(ILoggerFactory loggerFactory) : IAppLogger<TCategory>
{
    private readonly ILogger<TCategory> _logger = loggerFactory.CreateLogger<TCategory>();

    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation("{Message}", args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning("{Message}", args);
    }

    public void LogError(string message, params object[] args)
    {
        _logger.LogError("{Message}", args);
    }
}
