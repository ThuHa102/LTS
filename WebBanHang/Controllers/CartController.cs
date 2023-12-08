using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Entities;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICart cartServices;
        public CartController()
        {
            cartServices = new CartServices();
        }
        [HttpPost("AddCart")]
        [Authorize(Roles = "Admin")]
        public IActionResult AddCart(Carts carts)
        {
            var res = cartServices.AddCart(carts);
            if (res == Message.ErrorMessages.ThanhCong) return Ok(carts);
            if (res == Message.ErrorMessages.ChuaTonTai) return Ok("User chưa tồn tại");
            return Ok(res);
        }
        [HttpPut("UpdateCart")]
        public IActionResult UpdateCart(Carts carts)
        {
            var res = cartServices.UpdateCart(carts);
            if (res == Message.ErrorMessages.ThanhCong) return Ok(carts);
            if (res == Message.ErrorMessages.ChuaTonTai1) return Ok("User chưa tồn tại");
            if (res == Message.ErrorMessages.ChuaTonTai2) return Ok("Cart chưa tồn tại");

            return Ok(res);
        }
        [HttpDelete("DeleteCart")]
        public IActionResult DeleteCart(int cartId)
        {
            var res = cartServices.DeleteCart(cartId);
            if (res == Message.ErrorMessages.ThanhCong) return Ok("Xóa thành công ");
            if (res == Message.ErrorMessages.ChuaTonTai) return Ok("Cart chưa tồn tại");
            return Ok(res);
        }
    }
}
