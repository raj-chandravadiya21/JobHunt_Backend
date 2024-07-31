using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using JobHunt.Application.Interfaces.ChatInterface;
using JobHunt.Domain.DataModels.Response.Chat;
using JobHunt.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JobHunt.Application.Services.ChatService
{
    public class ChatServices(IUnitOfWork unitOfWork, IMapper mapper, Cloudinary cloudinary) : IChatService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly Cloudinary _cloudinary = cloudinary;

        public async Task<List<ChatResponse>> GetMessage(int conversationId)
        {
            List<ChatModel> chatData = await _unitOfWork.Message.GetChat(conversationId);

            List<ChatResponse> chatResponse = new();

            foreach (var chat in chatData)
            {
                var response = _mapper.Map<ChatResponse>(chat);

                if (!string.IsNullOrEmpty(chat.Content))
                {
                    try
                    {
                        response.Contents = JsonConvert.DeserializeObject<MessageModel>(chat.Content);
                    }
                    catch (Exception)
                    {
                        response.Contents = new();
                    }
                }

                chatResponse.Add(response);
            }

            return chatResponse;
        }

        public async Task<ChatAttachmentResponse> Upload(IFormFile file, string thumbnail)
        {
            var uploadParams = new RawUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = "uploads"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            string? thumbnailUrl = null;
            if (!string.IsNullOrEmpty(thumbnail))
            {
                var byteArray = Convert.FromBase64String(thumbnail.Split(",")[1]);
                using var stream = new MemoryStream(byteArray);

                var thumbnailUploadParams = new RawUploadParams()
                {
                    File = new FileDescription("thumbnail.png", stream),
                    Folder = "thumbnails"
                };

                var thumbnailUploadResult = await _cloudinary.UploadAsync(thumbnailUploadParams);

                thumbnailUrl = thumbnailUploadResult.SecureUrl.ToString();
            }

            ChatAttachmentResponse response = new()
            {
                Url = uploadResult.SecureUrl.ToString(),
                FileName = file.FileName,
                FileSize = file.Length,
                FileType = file.ContentType,
                ThumbnailUrl = thumbnailUrl!
            };

            return response;
        }
    }
}

