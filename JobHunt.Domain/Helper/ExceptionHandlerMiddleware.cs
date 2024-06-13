using JobHunt.Domain.DataModels.Response;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace JobHunt.Domain.Helper
{
    public class ExceptionHandlerMiddleware(RequestDelegate next)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex).ConfigureAwait(false);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            var innerException = exception.InnerException;
            var error = $"Exception is thrown ({exception.GetType()}): {exception.Message} \n ";

            var errorMessages = new List<string>();

            if (innerException != null)
            {
                error += $"Inner Exception ({innerException.GetType()}) : {innerException.Message}";
                errorMessages.Add(innerException.Message);
            }

            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(new ResponseDTO()
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = exception.Message,
                IsSuccess = false,
                Data = exception.Data,
                ErrorMessages = errorMessages
            });
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
