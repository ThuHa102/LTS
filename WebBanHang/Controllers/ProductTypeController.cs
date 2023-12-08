using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Entities;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductType productTypeServices;
        public ProductTypeController()
        {
            productTypeServices = new ProductTypeServices();
        }
        [HttpPost("/api/ProductType/ThemLoai")]
        [Authorize(Roles = "Admin")]
        public IActionResult ThemLoai([FromForm] Request_AddProductType request)
        {
            return Ok(productTypeServices.ThemLoai(request));
        }
        [HttpPut("/api/ProductType/SuaLoai")]
        public IActionResult SuaLoai([FromForm]ProductType productType)
        {
            return Ok(productTypeServices.SuaLoai(productType));
        }
        [HttpDelete("/api/ProductType/XoaLoai")]
        public IActionResult XoaLoai([FromForm]int productTypeId)
        {
            return Ok(productTypeServices.XoaLoai(productTypeId));  
        }
    }
}
