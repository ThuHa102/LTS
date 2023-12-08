using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponseProductReview
    {
        public int ProductReviewId { get; set; }
        public string NameProduct { get; set; }
        public string UserName { get; set; }
        public string ContentRated { get; set; }
        public int PointEvaluation { get; set; }
        public string ContentSeen { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
