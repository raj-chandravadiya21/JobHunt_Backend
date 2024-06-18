using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using JobHunt.Domain.Helper;
using Microsoft.Extensions.Configuration;

namespace JobHunt.Application.Services
{
    public class AuthService() : IAuthService
    {

                await _unitOfWork.User.CreateAsync(user);
                await _unitOfWork.SaveAsync();

                return new()
                {
                    IsSuccess = true,
                    Message = "User Created Successfully",
                    StatusCode = HttpStatusCode.OK,
                };
            }
            else
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "Otp Not Match",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
        }


        public static Task<string> GenerateOTP()
        {
            int length = 6;
            const string chars = "0123456789";

            var random = RandomNumberGenerator.Create();
            var bytes = new byte[length * sizeof(char)];
            random.GetBytes(bytes);

            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = chars[bytes[i] % chars.Length];
            }
            return Task.FromResult(new string(result));
        }

        public async Task<int> GenerateAndSaveOtp(string email)
        {
            var generatOtp = await GenerateOTP();

            int otp = Int32.Parse(generatOtp);

            OtpRecord otpRecord = new()
            {
                Email = email,
                Otp = otp,
                SentDatetime = DateTime.Now,
            };

            await _unitOfWork.OtpRecord.CreateAsync(otpRecord);
            await _unitOfWork.SaveAsync();

            return otp;
        }
    }
}
