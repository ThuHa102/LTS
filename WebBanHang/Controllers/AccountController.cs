using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount accountService;
        public AccountController(IAccount _accountService)
        {
            accountService = _accountService;
        }
        [HttpPost("/api/auth/register")]
        public IActionResult Register([FromForm]Request_Register request)
        {
            var res = accountService.Register(request);
            return Ok(res);
        }
        [HttpPost("/api/auth/login")]
        public IActionResult Login(Request_Login login)
        {
            return Ok(accountService.Login(login));
        }
        [HttpPost("/api/ConfirmAcc")]
        public IActionResult ConfirmAcc([FromForm] Request_ConfirmAcc request)
        {
            return Ok(accountService.ConfirmAccount(request));
        }
        [HttpPost("/api/ForotPassword")]
        public IActionResult ForgotPassword(Request_ForgotPassword request)
        {
            return Ok(accountService.ForgotPassword(request));
        }
        [HttpPost("/api/ChangePassword")]
        public IActionResult ChangePassword (Request_ChangePassword request)
        {
            return Ok(accountService.ChangePassword(request));
        }
    }
}
