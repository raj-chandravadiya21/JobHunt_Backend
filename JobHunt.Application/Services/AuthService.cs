using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.DataModels.Response;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Infrastructure.Interfaces;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Transactions;

namespace JobHunt.Application.Services
{
    public class AuthService(IUnitOfWork _unitOfWork, IMapper _mapper, IEmailSender _emailSender) : IAuthService
    {
        public async Task<ResponseDTO> CheckUser(CheckEmailDTO model)
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

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Aspnetuser aspUser = new()
                    {
                        CreatedDate = DateTime.Now,
                        RoleId = (int)Role.User,
                        Email = model.Email!,
                        FirstName = model.FirstName!,
                        LastName = model.LastName
                    };

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

                    transactionScope.Complete();
                }

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

        public async Task<ResponseDTO> Login(LoginAspNetUserDTO model, int role)
        {
            var aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(u => u.Email == model.Email && u.RoleId == role);

            if (aspnetuser == null)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "Email Id does not exists!",
                    StatusCode = HttpStatusCode.OK,
                };
            }

            if (BCrypt.Net.BCrypt.Verify(model.Password, aspnetuser.Password))
            {
                var claims = new Claim[]
                {
                    new(ClaimTypes.Role,(((Role)aspnetuser.RoleId!).ToString())),
                    new(ClaimTypes.Sid,aspnetuser.AspnetuserId.ToString())
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

        public async Task<ResponseDTO> RegisterCompany(RegisterCompanyDTO model)
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

                string salt = BCrypt.Net.BCrypt.GenerateSalt();
                string password = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    Aspnetuser aspUser = new()
                    {
                        FirstName = model.CompanyName,
                        Password = password,
                        Email = model.Email!,
                        RoleId = (int)Role.Company,
                        CreatedDate = DateTime.Now,
                    };

                    await _unitOfWork.AspNetUser.CreateAsync(aspUser);
                    await _unitOfWork.SaveAsync();

                    Company company = new()
                    {
                        AspnetuserId = aspUser.AspnetuserId,
                        CompanyName = model.CompanyName,
                        Email = model.Email!,
                        CreatedDate = DateTime.Now,
                        IsApprove = false,
                    };

                    await _unitOfWork.Company.CreateAsync(company);
                    await _unitOfWork.SaveAsync();
                    transactionScope.Complete();
                }

                return new()
                {
                    IsSuccess = true,
                    Message = "Company Registered Successfully",
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

        public async Task<ResponseDTO> ForgotPasswordUser(ForgotPasswordDTO model)
        {
            var user = await _unitOfWork.AspNetUser.GetFirstOrDefault(u => u.Email == model.Email && u.RoleId == model.Role);
            if (user == null)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "User Not Found",
                    StatusCode = HttpStatusCode.OK,
                };
            }

            var expires = DateTime.UtcNow.AddMinutes(20);
            var unixExpires = new DateTimeOffset(expires).ToUnixTimeSeconds();

            var claims = new Claim[]
                {
                    new(ClaimTypes.Role,(((Role)model.Role!).ToString())),
                    new(ClaimTypes.Sid,user.AspnetuserId.ToString()),
                    new(ClaimTypes.Expiration,unixExpires.ToString())
                };

            var token = Jwt.GenerateToken(claims, DateTime.Now.AddMinutes(20));

            await _emailSender.SendResetPasswordLink(token, user.Email);

            return new()
            {
                IsSuccess = true,
                Message = "Reset Password link sent Successfully",
                StatusCode = HttpStatusCode.OK
            };
        }

        public async Task<ResponseDTO> ValidateResetToken(ValidateTokenDTO model)
        {
            bool isValid = await ValidToken(model.Token!);
            if (isValid)
            {
                return new()
                {
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK,
                    Message = "Token is valid"
                };
            }
            return new()
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.OK,
                Message = "Token is not Valid"
            };
        }

        public async Task<bool> ValidToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            return await Task.FromResult(Jwt.ValidateToken(token, out JwtSecurityToken? _));
        }
    }
}
