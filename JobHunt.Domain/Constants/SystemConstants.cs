using JobHunt.Domain.Helper;

namespace JobHunt.Domain.Constants
{
    public static class SystemConstants
    {
        #region Jwt
        public static readonly string JWT_KEY = ConfigurationHelper.GetConfigurationSection("Jwt")["Key"]!;  

        public static readonly string JWT_ISSUER = ConfigurationHelper.GetConfigurationSection("Jwt")["Issuer"]!;

        public static readonly string JWT_AUDIENCE = ConfigurationHelper.GetConfigurationSection("Jwt")["Audience"]!;
        #endregion

        #region Encryption
        public static readonly string SHA_KEY = ConfigurationHelper.GetConfigurationSection("SHAEncryption")["Key"]!;

        public static readonly string SHA_IV = ConfigurationHelper.GetConfigurationSection("SHAEncryption")["IV"]!;
        #endregion
    }
}
