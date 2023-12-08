using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IProductReview
    {
        public IEnumerable<DataResponseProductReview> GetAll(int productReviewId, int pagaSize, int pageNumber);
        ResponseMessage AddProductReview(Request_AddProductReview request);
    }
}
