using Microsoft.Extensions.Configuration;

namespace JobHunt.Domain.Helper
{
    public static class ConfigurationHelper
    {
        private static IConfiguration _configuration = null!;

        public static void Configure(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static IConfigurationSection GetConfigurationSection(string section)
        {
            return _configuration.GetSection(section);
        }
    }
}
