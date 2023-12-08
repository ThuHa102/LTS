using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder orderServices;
        public OrderController()
        {
            orderServices = new OrderServices();
        }
        [HttpPost("/api/order/addorder")]
        public IActionResult AddOrder(Request_AddOrder request)
        {
            return Ok(orderServices.AddOrder(request));
        }
        [HttpPost("/api/doanhthuthang")]
        public IActionResult TinhDoanhThuThang(int thang, int nam)
        {
            return Ok(orderServices.TinhDoanhThuThang(thang, nam));
        }
        [HttpPost("/api/doanhthuquy")]
        public IActionResult TinhDoanhThuQuy(int quy, int nam)
        {
            return Ok(orderServices.TinhDoanhThuQuy(quy, nam));
        }
        [HttpPost("api/doanhthunam")]
        public IActionResult TinhDoanhThuNam(int nam)
        {
            return Ok(orderServices.TinhDoanhThuNam(nam));
        }
        [HttpGet ("/api/order/getall")]
        public IActionResult GetAll (int pageSize, int pageNumber)
        {
            return Ok(orderServices.GetAll(pageSize, pageNumber));
        }
        //[HttpPut("/api/order/capnhattrangthai")]
        //public IActionResult CapNhatTrangThaiDonHang (int orderId, int orderStatusId)
        //{
        //    return Ok(orderServices.CapNhatTrangThaiDonHang(orderId, orderStatusId));
        //}
    }
}
