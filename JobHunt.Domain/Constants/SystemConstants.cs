using JobHunt.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.Constants
{
    public class SystemConstants
    {
        #region Jwt
        public static string JWT_KEY = ConfigurationHelper.GetConfigurationSection("Jwt")["Key"];

        public static string JWT_ISSUER = ConfigurationHelper.GetConfigurationSection("Jwt")["Issuer"];

        public static string JWT_AUDIENCE = ConfigurationHelper.GetConfigurationSection("Jwt")["Audience"];

        #endregion

    }
}