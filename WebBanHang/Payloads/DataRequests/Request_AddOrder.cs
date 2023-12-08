using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataRequests
{
    public class Request_AddOrder
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string address { get; set; }
        public List<Request_AddOrderDetail> request_AddOrderDetails { get; set; }
    }
}
