using JobHunt.Domain.Resource;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobHunt.Domain.Helper
{
    public static class GetTokenFromHeader
    {
        public static string GetToken(IHttpContextAccessor http)
        {
            var token = http.HttpContext!.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last()!;

            if(token != null)
            {
                return token;
            }
            throw new CustomException(Messages.DataNotFound);
        }
    }
}
