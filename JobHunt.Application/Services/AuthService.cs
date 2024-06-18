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

namespace JobHunt.Application.Services
{
    public class AuthService(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailSender _emailSender) : IAuthService
    {


        public async Task<ResponseDTO> CheckUser(CheckUserDTO model)
        {
            var user = await _unitOfWork.AspNetUser.GetAnyAsync(u => u.Email == model.Email && u.RoleId == (int)Role.User);
            if ((bool)user)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "User Already Exists",
                    StatusCode = HttpStatusCode.OK,
                };
            }

            int otp = await GenerateAndSaveOtp(model.Email!);

            await _emailSender.SendEmailVerifiaction(otp, model.Email!);

            return new()
            {
                IsSuccess = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Otp Sent Successfully",
            };
        }

        public async Task<ResponseDTO> RegisterUser(RegisterUserDTO model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "Password and Confirm Password Not match",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }

            var otpRecord = _unitOfWork.OtpRecord.GetLastOrDefaultOrderedBy(u => u.Email == model.Email, u => u.SentDatetime);

            int otp = otpRecord.Result!.Otp;

            if (otp == model.Otp)
            {
                if (DateTime.Now.AddMinutes(-20) >= otpRecord.Result!.SentDatetime)
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Time Out",
                        StatusCode = HttpStatusCode.BadRequest,
                    };
                }

                Aspnetuser aspUser = new Aspnetuser();
                aspUser.CreatedDate = DateTime.Now;
                aspUser.RoleId = (int)Role.User;
                aspUser.Email = model.Email!;
                aspUser.FirstName = model.FirstName!;
                aspUser.LastName = model.LastName;

                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string Password = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);

                aspUser.Password = Password;

                await _unitOfWork.AspNetUser.CreateAsync(aspUser);
                await _unitOfWork.SaveAsync();

                User user = new()
                {
                    FirstName = model.FirstName!,
                    LastName = model.LastName,
                    Email = model.Email!,
                    AspnetuserId = aspUser.AspnetuserId,
                    Createddate = DateTime.Now,
                };

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

        public async Task<ResponseDTO> LoginUser(LoginUserDTO model)
        {
            var user = await _unitOfWork.AspNetUser.GetFirstOrDefault(u => u.Email == model.Email && u.RoleId == (int)Role.User);

            if (user == null)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "User Not Found",
                    StatusCode = HttpStatusCode.OK,
                };
            }

            if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var claims = new Claim[]
                {
                    new(ClaimTypes.Role,(((Role)user.RoleId!).ToString())),
                    new(ClaimTypes.Sid,user.AspnetuserId.ToString())
                };

                var token = Jwt.GenerateToken(claims, DateTime.Now.AddDays(1));

                return new()
                {
                    Data = new
                    {
                        token,
                    },
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Login Successfully",
                };
            }

            return new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                Message = "Enter valid Crendential",
            };
        }
    }
}
