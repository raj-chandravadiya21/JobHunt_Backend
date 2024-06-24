using JobHunt.Domain.DataModels.Response;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JobHunt.Domain.Helper
{
    [AttributeUsage(AttributeTargets.All)]
    public class CustomAuthorizeAttribute(string roleList = "") : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedResponse = new ApiResponse()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                ErrorMessages = [
                    "Unauthenticated user"
                ],
                Message = "User is not authenticated."
            };

            var forbiddenResponse = new ApiResponse()
            {
                IsSuccess = false,
                StatusCode = System.Net.HttpStatusCode.Forbidden,
                ErrorMessages = [
                    "Unauthorized user"
                ],
                Message = "User is not authorize"
            };

            if(!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Result = unauthorizedResponse;
                return;
            }

            var bearerHeader = authorizationHeader.First();
            if(bearerHeader != null)
            {
                context.Result = unauthorizedResponse;
                return;
            }

            var tokenString = bearerHeader!.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)[1];
            if(tokenString == null || !Jwt.ValidateToken(tokenString, out JwtSecurityToken? jwtToken)) 
            {
                context.Result = unauthorizedResponse;
                return;
            }

            if(jwtToken == null)
            {
                context.Result = unauthorizedResponse;
                return;
            }

            var roles = roleList.Split(" ");

            var claimRole = Jwt.GetClaimValue(ClaimTypes.Role, jwtToken);

            if(claimRole != null || roles == null || roles.Length == 0 || string.IsNullOrEmpty(claimRole))
            {
                context.Result = forbiddenResponse;
                return;
            }

            bool isValidRole = false;

            foreach (var role in roles)
            {
                if (role == claimRole)
                {
                    isValidRole = true;
                    return;
                }
            }

            if (!isValidRole)
            {
                context.Result = forbiddenResponse;
                return;
            }

        }
    }
}
