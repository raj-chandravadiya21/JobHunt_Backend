using AutoMapper;
using JobHunt.Application.Interfaces;
using JobHunt.Domain.DataModels.Request;
using JobHunt.Domain.Entities;
using JobHunt.Domain.Enum;
using JobHunt.Domain.Helper;
using JobHunt.Domain.Resource;
using JobHunt.Infrastructure.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Transactions;

namespace JobHunt.Application.Services
{
    public class AuthService(IUnitOfWork _unitOfWork, IEmailSender _emailSender) : IAuthService
    {
        #region Check-user & send OTP
        public async Task CheckUser(CheckEmailRequest model)
        {
            if ((bool)await _unitOfWork.AspNetUser.GetAnyAsync(u => u.Email == model.Email && u.RoleId == (int)Role.User))
            {
                throw new AlreadyExistsException("Email Already Registered With Same Email");
            }

            int otp = await GenerateAndSaveOtp(model.Email!);

            await _emailSender.SendEmailVerifiaction(otp, model.Email!);
        }
        #endregion

        #region RegisterUser
        public async Task RegisterUser(RegisterUserRequest model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new CustomException(string.Format(Messages.PasswordConfirmPasswordNotMatch));
            }

            var otpRecord = await _unitOfWork.OtpRecord.GetLastOrDefaultOrderedBy(u => u.Email == model.Email, u => u.SentDatetime);

            if (otpRecord!.Otp == model.Otp)
            {
                if (DateTime.Now.AddMinutes(-20) >= otpRecord!.SentDatetime)
                {
                    throw new CustomException(string.Format(Messages.TimeExpire));
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
            }
            else
            {
                throw new CustomException(string.Format(Messages.OtpNotMatch));
            }
        }
        #endregion

        #region GenerateOTP
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
        #endregion

        #region GenerateAndSaveOTP
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
        #endregion

        #region Login
        public async Task<string> Login(LoginRequest model, int role)
        {
            var aspnetuser = await _unitOfWork.AspNetUser.GetFirstOrDefault(u => u.Email == model.Email && u.RoleId == role);

            if (BCrypt.Net.BCrypt.Verify(model.Password, aspnetuser!.Password))
            {
                var claims = new Claim[]
                {
                    new(ClaimTypes.Role,(((Role)aspnetuser.RoleId!).ToString())),
                    new(ClaimTypes.Sid,aspnetuser.AspnetuserId.ToString())
                };

                var token = Jwt.GenerateToken(claims, DateTime.Now.AddDays(1));

                return token;
            }
            else
            {
                throw new CustomException(string.Format(Messages.EnterValidCredentials));
            }
        }
        #endregion

        #region check-company-sendOTP
        public async Task CheckCompany(CheckEmailRequest model)
        {
            if ((bool)await _unitOfWork.AspNetUser.GetAnyAsync(u => u.Email == model.Email && u.RoleId == (int)Role.Company))
            {
                throw new AlreadyExistsException("Email Already Registered With Same Email");
            }

            int otp = await GenerateAndSaveOtp(model.Email!);

            await _emailSender.SendEmailVerifiaction(otp, model.Email!);
        }
        #endregion

        #region Register-company
        public async Task RegisterCompany(RegisterCompanyRequest model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                throw new CustomException(string.Format(Messages.PasswordConfirmPasswordNotMatch));
            }

            var otpRecord = await _unitOfWork.OtpRecord.GetLastOrDefaultOrderedBy(u => u.Email == model.Email, u => u.SentDatetime);

            if (otpRecord!.Otp == model.Otp)
            {
                if (DateTime.Now.AddMinutes(-20) >= otpRecord!.SentDatetime)
                {
                    throw new CustomException(string.Format(Messages.TimeExpire));
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
            }
            else
            {
                throw new CustomException(string.Format(Messages.OtpNotMatch));
            }
        }
        #endregion

        #region ForgotPassword
        public async Task ForgotPasswordUser(ForgotPasswordRequest model)
        {
            Aspnetuser? user = await _unitOfWork.AspNetUser.GetFirstOrDefault(u => u.Email == model.Email && u.RoleId == model.Role);

            var aspNetUserId = SHAHelper.EncryptionHelper.Encrypt(user!.AspnetuserId.ToString());

            var claims = new Claim[]
                {
                    new(ClaimTypes.Sid,aspNetUserId),
                    new(ClaimTypes.Expiration,user.Password.ToString())
                };

            var token = Jwt.GenerateToken(claims, DateTime.Now.AddMinutes(20));

            await _emailSender.SendResetPasswordLink(token, user.Email);
        }
        #endregion

        #region ValidateResetToken
        public Task ValidateResetToken(ValidateTokenRequest model)
        {
            var isValid = Jwt.ValidateToken(model.Token!, out JwtSecurityToken? _);
            if (!isValid)
            {
                throw new CustomException(string.Format(Messages.TimeExpire, Messages.ResetPasswordLink));
            }

            return Task.CompletedTask;
        }
        #endregion

        #region ResetPassword
        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var isValidToken = Jwt.ValidateToken(model.Token!, out JwtSecurityToken? jwtToken);
            if (!isValidToken)
            {
                throw new CustomException(string.Format(Messages.TimeExpire, Messages.ResetPasswordLink));
            }

            var userPassword = Jwt.GetClaimValue(ClaimTypes.Expiration, jwtToken!);
            var encryorionUserId = Jwt.GetClaimValue(ClaimTypes.Sid, jwtToken!);

            var userId = SHAHelper.EncryptionHelper.Decrypt(encryorionUserId!);

            var user = await _unitOfWork.AspNetUser.GetFirstOrDefault(u => u.AspnetuserId.ToString() == userId);

            if (userPassword != user!.Password)
            {
                throw new CustomException(string.Format(Messages.TimeExpire, Messages.ResetPasswordLink));
            }

            if (model.Password != model.ConfirmPassword)
            {
                throw new CustomException(string.Format(Messages.PasswordConfirmPasswordNotMatch));
            }

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string password = BCrypt.Net.BCrypt.HashPassword(model.Password, salt);

            user.Password = password;
            user.ModifiedDate = DateTime.Now;


            _unitOfWork.AspNetUser.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
        }
        #endregion
    }
}