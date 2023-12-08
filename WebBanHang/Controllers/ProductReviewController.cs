using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Services.Implements;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReview productReviewServices;
        public ProductReviewController()
        {
            productReviewServices = new ProductReviewServices();
        }
        [HttpGet("/api/product_review/getall")]
        public IActionResult GetAll(int productReviewId, int pageSize, int pageNumber)
        {
            return Ok(productReviewServices.GetAll(productReviewId, pageSize, pageNumber));
        }
        [HttpPost("/api/ProductReview/Add")]
        public IActionResult AddProductReview (Request_AddProductReview request)
        {
            return Ok(productReviewServices.AddProductReview(request));
        }
    }
}
