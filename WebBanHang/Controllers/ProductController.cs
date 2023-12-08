using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct productServices;
        public ProductController()
        {
            productServices = new ProductServices();
        }
        [HttpGet("/api/product/getall")]
        public IActionResult GetAll(int pageSize, int pageNumber)
        {
            return Ok(productServices.GetAll(pageSize, pageNumber));
        }
        [HttpGet("/api/product/xemchitietsanpham")]
        public IActionResult XemChiTietSanPham(int productId)
        {
            return Ok(productServices.XemChiTietSanPham(productId));
        }
        [HttpGet("/api/product/hienthisanphamlienquan")]
        public IActionResult HienThiSanPhamLienQuan(int productId, int pageSize, int pageNumber)
        {
            return Ok(productServices.HienThiSanPhamLienQuan(productId, pageSize, pageNumber));
        }
        [HttpGet("/api/product/hienthisanphamnoibat")]
        public IActionResult HienThiSanPhamNoiBat(int top, int pageSize, int pageNumber)
        {
            return Ok(productServices.HienThiSanPhamNoiBat(top, pageSize, pageNumber));
        }
    }
}
