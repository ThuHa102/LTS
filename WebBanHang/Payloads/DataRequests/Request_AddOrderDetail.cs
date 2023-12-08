using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataRequests
{
    public class Request_AddOrderDetail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
