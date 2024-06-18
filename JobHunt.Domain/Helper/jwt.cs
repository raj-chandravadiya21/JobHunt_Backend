using JobHunt.Domain.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace JobHunt.Domain.Helper
{
    public class Jwt
    {
        public static string GenerateToken(IEnumerable<Claim> claims, DateTime expires)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConstants.JWT_KEY));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                SystemConstants.JWT_ISSUER,
                SystemConstants.JWT_AUDIENCE,
                claims,
                expires: expires,
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static bool IsTokenValidResetPassword(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConstants.JWT_KEY));

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var expirationDateUnix = long.Parse(jwtToken.Claims.First(x => x.Type == ClaimTypes.Expiration).Value);
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationDateUnix).UtcDateTime;

                return (expirationDateTime > DateTime.UtcNow);
            }
            catch(SecurityTokenExpiredException)
            {
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while validating the token: {ex.Message}");
                return false;
            }
        }

        public static bool ValidateToken(string token, out JwtSecurityToken? jwtSecurityToken)
        {
            jwtSecurityToken = null;

            if(token == null)
            {
                return false;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SystemConstants.JWT_KEY));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;

                if(jwtSecurityToken == null)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string? GetClaimValue(string claimType, JwtSecurityToken token)
        {
            var claim = token.Claims.FirstOrDefault(a => a.Type == claimType);
            if(claim != null)
            {
                return claim.Value;
            }
            return null;
        }
    }
}
