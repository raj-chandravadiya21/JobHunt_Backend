using JobHunt.Domain.DataModels.Response;
using System.Net;

namespace JobHunt.Domain.Helper
{
    public static class ResponseHelper
    {
        public static ApiResponse SuccessResponse(object data, string? message = null)
        {
            return new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Data = data,
                Message = !string.IsNullOrEmpty(message) ? message : "Success"
            };
        }

        public static ApiResponse CreateResponse(object data, string? message = null)
        {
            return new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.Created,
                Data = data,
                Message = !string.IsNullOrEmpty(message) ? message : "Created"
            };
        }
    }
}