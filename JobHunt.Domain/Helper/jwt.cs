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
                    ClockSkew = TimeSpan.Zero,
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
