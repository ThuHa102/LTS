namespace WebBanHang.Payloads.DataRequests
{
    public class Request_AddProductReview
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public string ContentRated { get; set; }
        public int PointEvaluation { get; set; }
        public string ContentSeen { get; set; }
    }
}
