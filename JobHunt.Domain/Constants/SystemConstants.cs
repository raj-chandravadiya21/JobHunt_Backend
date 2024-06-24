using JobHunt.Domain.Helper;

namespace JobHunt.Domain.Constants
{
    public static class SystemConstants
    {
        #region Jwt
        public static string JWT_KEY = ConfigurationHelper.GetConfigurationSection("Jwt")["Key"];

        public static string JWT_ISSUER = ConfigurationHelper.GetConfigurationSection("Jwt")["Issuer"];

        public static string JWT_AUDIENCE = ConfigurationHelper.GetConfigurationSection("Jwt")["Audience"];

        #endregion

    }
}