using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JobHunt.Domain.DataModels.Response
{
    public class ApiResponse : ActionResult
    {

        [JsonPropertyName("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; } = null!;

        [JsonPropertyName("statusCode")]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = null!;

        [JsonPropertyName("errorMessages")]
        public List<string> ErrorMessages { get; set; } = new List<string>();


        public override void ExecuteResult(ActionContext context)
        {
            var res = context.HttpContext.Response;
            res.ContentType = "application/json";
            res.StatusCode = (int)StatusCode;
            res.WriteAsync(JsonSerializer.Serialize(new
            {
                isSuccess = IsSuccess,
                data = Data,
                message = Message,
                statusCode = StatusCode,
                errorMessages = ErrorMessages
            }));
        }
    }
}
