using Microsoft.Extensions.Logging;

namespace JobHunt.Domain.Helper
{
    public static class LogHelper
    {
        private static ILogger _logger = null;

        public static void SetLogger(ILogger logger)
        {
            _logger = logger;   
        }

        public static void LogInformation(string message)
        {
            _logger.LogInformation("{Message}",message);
        }

        public static void LogError(string message)
        {
            _logger.LogError("{Message}", message);
        }

        public static void LogWarning(string message)
        {
            _logger.LogWarning("{Message}", message);
        }
    }
}
