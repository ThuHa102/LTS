using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Services.Implements;

namespace WebBanHang.Payloads.Converters
{
    public class ProductReviewConverter : BaseServices
    {
        public DataResponseProductReview ProductReviewToDTO (ProductReview productReview)
        {
            return new DataResponseProductReview()
            {
                NameProduct = context.Products.FirstOrDefault(x => x.ProductId == productReview.ProductId).NameProduct,
                UserName = context.Users.FirstOrDefault(x=>x.UserId == productReview.UserId).UserName,
                ContentRated = productReview.ContentRated,
                ContentSeen = productReview.ContentSeen,
                PointEvaluation = productReview.PointEvaluation,
                Status = productReview.Status
            };
        }
    }
}
