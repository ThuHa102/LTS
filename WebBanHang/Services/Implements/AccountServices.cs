using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using WebBanHang.Entities;
using WebBanHang.Handle.Email;
using WebBanHang.Payloads.Converters;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;
using WebBanHang.Services.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebBanHang.Services.Implements
{
    public class AccountServices : BaseServices, Interfaces.IAccount
    {
        private readonly ResponseObject<DataResponsesAcc> responseObject;
        private readonly AccConverter accConverter;
        private readonly IConfiguration configuration;
        private readonly ResponseObject<DataResponseToken> responseTokenObject;
        private readonly ResponseMessage responseMessage;
        public AccountServices(IConfiguration _configuration)
        {
            responseObject = new ResponseObject<DataResponsesAcc>();
            accConverter = new AccConverter();
            configuration = _configuration;
            responseTokenObject = new ResponseObject<DataResponseToken>();
            responseMessage = new ResponseMessage();
        }

        public ResponseObject<DataResponsesAcc> Register(Request_Register request)
        {
            if(string.IsNullOrWhiteSpace(request.AccName) 
                || string.IsNullOrWhiteSpace(request.Email)
                || string.IsNullOrWhiteSpace(request.Password))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Vui long nhap day du thong tin", null);
            }
            if (context.Accounts.Any(x => x.AccName.Equals(request.AccName)))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Ten tai khoan da ton tai", null);
            }
            if(!Validate.IsValidEmail(request.Email))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Dinh dang Email khong hop le", null);
            }
            if (context.Accounts.Any(x => x.Email.Equals(request.Email)))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Email da duoc su dung vi tai khoan khac",null);
            }
            var acc = new Account();
            acc.AccName = request.AccName;
            acc.Password = BCryptNet.HashPassword(request.Password);
            acc.DecentralizationId = 3;
            acc.CreatedAt = DateTime.Now;
            acc.status = 0;
            acc.Email = request.Email;
            string verificationCode = GenerateVerificationCode();
            acc.ConfirmAcc = verificationCode;
            context.Accounts.Add(acc);
            context.SaveChanges();
            string email = acc.Email;
            SendVerificationEmail(email);
            DataResponsesAcc result = accConverter.EntityToDTO(acc);
            return responseObject.ThanhCong("Dang ky thanh cong. Vui long kiem tra email de nhan ma xac nhan", result);

        }
        private string GenerateRefreshToken()
        {
            var random = new byte[32];
            using(var item = RandomNumberGenerator.Create())
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public DataResponseToken GenerateAccessToken(Account account)
        {
            var jwtTokenHendler = new JwtSecurityTokenHandler();
            var seretKeyBytes = System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:SecretKey").Value);
            var role = context.Accounts.Include(x => x.Decentralization).FirstOrDefault(x => x.DecentralizationId == account.DecentralizationId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", account.AccountId.ToString()),
                    new Claim(ClaimTypes.Role, role?.Decentralization.AuthorityName ?? ""),
                }),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(seretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHendler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHendler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            RefreshToken refresh = new RefreshToken
            {
                Token = refreshToken,
                ExpiryTime = DateTime.Now.AddDays(1),
                AccountId = account.AccountId
            };
            context.RefreshTokens.Add(refresh);
            context.SaveChanges();
            DataResponseToken result = new DataResponseToken
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return result;
        }

        public DataResponseToken RenewAccessToken(Request_RenewAccessToken request)
        {
            throw new NotImplementedException();
        }

        public ResponseObject<DataResponseToken> Login(Request_Login request)
        {
            var account = context.Accounts.FirstOrDefault(x=>x.AccName.Equals(request.AccName));
            if(string.IsNullOrWhiteSpace(request.AccName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return responseTokenObject.ThatBai(StatusCodes.Status400BadRequest, "Vui lòng nhập đầy đủ thông tin", null);
            }
            if (!context.Accounts.Any(x => x.AccName.Equals(request.AccName)))
            {
                return responseTokenObject.ThatBai(StatusCodes.Status400BadRequest, "Tài khoản chưa tồn tại", null);
            }
            bool checkPass = BCryptNet.Verify(request.Password,account.Password);
            if (!checkPass)
            {
                return responseTokenObject.ThatBai(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác", null);
            }
            return responseTokenObject.ThanhCong("Đăng nhập thành công", GenerateAccessToken(account));
        }
        public string GenerateVerificationCode()
        {
            // Tạo mã xác minh ngẫu nhiên (ví dụ: 6 chữ số)
            Random random = new Random();
            int code = random.Next(100000, 999999);
            return code.ToString();
        }
        public void SendVerificationEmail(string email)
        {
            //MailMessage message = new MailMessage();
            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");

            //// Cấu hình thông tin email gửi đi
            //message.From = new MailAddress(ConstanceHelper.emailSender);
            //message.To.Add(new MailAddress(email));
            //message.Subject = "Xác minh tài khoản";
            //message.Body = $"Mã xác minh của bạn là: {"Ha__" + DateTime.Now.Ticks.ToString() + new Random().Next(1000,9999).ToString()}";

            //// Cấu hình thông tin SMTP server
            //smtpClient.Host = ConstanceHelper.hostEmail;
            //smtpClient.Port = 587;
            //smtpClient.EnableSsl = true;
            //smtpClient.UseDefaultCredentials = true;
            //smtpClient.Credentials = new System.Net.NetworkCredential(ConstanceHelper.emailSender, "iesy cowv surg lfzq");

            //// Gửi email
            //smtpClient.Send(message);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("dangthuha01072k2@gmail.com", "cwku bvbd euzo qpzk"),
                EnableSsl = true
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("dangthuha01072k2@gmail.com");
                message.To.Add(email);
                message.Subject = "Xác minh tài khoản";
                var account = context.Accounts.SingleOrDefault(x => x.Email.Equals(email));
                message.Body = $"Mã xác minh của bạn là: {account.ConfirmAcc}";
                message.IsBodyHtml = true;
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(  ex.Message);
            }
        }
        public bool VerifyCode(string userCode, string verificationCode)
        {
            return userCode == verificationCode;
        }

        public ResponseMessage ConfirmAccount(Request_ConfirmAcc request)
        {
            var acc = context.Accounts.FirstOrDefault(x => x.AccName.Equals(request.AccName));
            string email = acc.Email;
            string verificationCode = acc.ConfirmAcc;
            SendVerificationEmail(email);
            bool isVerified = VerifyCode(request.ConfirmCode, verificationCode);
            if (isVerified)
            {
                acc.status = 1;
                context.Accounts.Update(acc);
                context.SaveChanges();
                return responseMessage.ThanhCong("Xac minh tai khoan thanh cong !");
            }
            else
            {
                return responseMessage.ThatBai("Ma xac minh khong hop le, vui long thu lai");
            }
        }

        public ResponseMessage ForgotPassword(Request_ForgotPassword request)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccName.Equals(request.AccName));
            if (account == null)
            {
               return responseMessage.ThatBai("Không tồn tại tài khoản này trên hệ thống !");
            }
            else
            {
                string verificationCode = GenerateVerificationCode();
                string email = account.Email;
                account.ResetPasswordToken = verificationCode;
                context.Update(account);
                context.SaveChanges();
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("dangthuha01072k2@gmail.com", "cwku bvbd euzo qpzk"),
                    EnableSsl = true
                };
                try
                {
                    var message = new MailMessage();
                    message.From = new MailAddress("dangthuha01072k2@gmail.com");
                    message.To.Add(email);
                    message.Subject = "Xác minh tài khoản";
                    message.Body = $"Mã xác minh của bạn là: {account.ResetPasswordToken}";
                    message.IsBodyHtml = true;
                    smtpClient.Send(message);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return responseMessage.ThanhCong("Gửi mã xác nhận thành công. Vui lòng kiểm tra Email.");
            }
        }

        public ResponseMessage ChangePassword(Request_ChangePassword request)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccName.Equals(request.AccName));
            if (account == null)
            {
                return responseMessage.ThatBai("Tài khoản không tồn tại trên hệ thống");
            }
            if(account.ResetPasswordToken != request.ResetPasswordToken)
            {
                return responseMessage.ThatBai("Mã xác nhận không chính xác !");
            }
            account.Password = BCryptNet.HashPassword(request.NewPassword);
            context.Accounts.Update(account);
            context.SaveChanges();
            return responseMessage.ThanhCong("Đổi mật khẩu thành công");
        }
    }
}
