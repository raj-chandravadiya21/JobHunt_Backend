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

            var errorMessages = new List<string>();

            if (innerException != null)
            {
                errorMessages.Add(innerException.Message);
            }

            HttpStatusCode statusCode;

            switch (exception)
            {
                case CustomException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;

                case NullObjectException:
                    statusCode = HttpStatusCode.NotFound;
                    break;

                case AlreadyExistsException:
                    statusCode = HttpStatusCode.Conflict;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";
            
            var result = JsonSerializer.Serialize(new ApiResponse()
            {
                StatusCode = statusCode,
                Message = exception.Message,
                IsSuccess = false,
                Data = exception.Data,
                ErrorMessages = errorMessages
            });
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
