using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebBanHang.Entities;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment paymentServices;
        private readonly AppDbContext _context;
        public Request_VNPayPayment request;

        public PaymentController(IPayment _paymentServices)
        {
            paymentServices = _paymentServices;
            _context = new AppDbContext();
        }
        [HttpPost]
        public IActionResult CreatePaymentUrl(Request_VNPayPayment model)
        {
            var url = paymentServices.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }
        [HttpGet]
        public IActionResult PaymentCallback()
        {
            var response = paymentServices.PaymentExecute(Request.Query, request);     
            return Ok(response);
        }
    }
}
