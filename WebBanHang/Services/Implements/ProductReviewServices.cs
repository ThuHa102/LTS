using WebBanHang.Entities;
using WebBanHang.Payloads.Converters;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Services.Implements
{
    public class ProductReviewServices : BaseServices, IProductReview
    {
        private ProductReviewConverter productReviewConverter;
        private ResponseMessage responseMessage;
        public ProductReviewServices()
        {
            productReviewConverter = new ProductReviewConverter();
            responseMessage = new ResponseMessage();
        }

        public IEnumerable<DataResponseProductReview> GetAll(int productReviewId, int pagaSize, int pageNumber)
        {
            var lst = context.ProductReviews.Where(x=>x.ProductReviewId == productReviewId).Skip(pageNumber-1).Take(pagaSize).Select(x=> productReviewConverter.ProductReviewToDTO(x)).ToList();
            return lst;
        }
        public ResponseMessage AddProductReview(Request_AddProductReview request)
        {
            if(context.Orders.Any(x=>x.UserId == request.UserId && x.OrderDetails.Any(x=>x.ProductId == request.ProductId) && x.OderStatusId==2))
            {
                var proReview = new ProductReview();
                proReview.ProductId = request.ProductId;
                proReview.UserId = request.UserId;
                proReview.ContentRated = request.ContentRated;
                proReview.PointEvaluation = request.PointEvaluation;
                proReview.ContentSeen = request.ContentSeen;
                proReview.Status = 1;
                proReview.CreatedAt = DateTime.Now;
                proReview.UpdateAt = DateTime.Now;
                context.ProductReviews.Add(proReview);
                context.SaveChanges();
                return responseMessage.ThanhCong("Đánh giá thành công");
            }
            else
            {
                return responseMessage.ThatBai("Người dùng chưa mua sản phẩm này !");
            }
        }
    }
}
