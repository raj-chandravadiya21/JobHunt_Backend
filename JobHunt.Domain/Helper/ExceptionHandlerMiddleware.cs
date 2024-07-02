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
                var ipAddress =  context.Connection.RemoteIpAddress?.ToString();

                LogHelper.LogInformation($"Incoming request from : {ipAddress}");

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

            LogHelper.LogError(error);

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
