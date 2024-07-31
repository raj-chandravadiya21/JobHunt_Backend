using JobHunt.Application.Interfaces;
using JobHunt.Domain.Helper;
using Microsoft.AspNetCore.Mvc;

namespace JobHunt.Controllers.Chat
{
    [Route("api/chat")]
    [ApiController]
    public class ChatController(IServiceBundle serviceBundle) : ControllerBase
    {
        private readonly IServiceBundle _serviceBundle = serviceBundle;

        [HttpGet("get-message")]
        public async Task<IResult> GetMessage(int conversationId)
        {
            var data = await _serviceBundle.ChatService.GetMessage(conversationId);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }

        [HttpPost("upload")]
        public async Task<IResult> Upload(IFormFile file, string thumbnail)
        {
            var data = await _serviceBundle.ChatService.Upload(file, thumbnail);
            return Results.Ok(ResponseHelper.SuccessResponse(data));
        }
    }
}
