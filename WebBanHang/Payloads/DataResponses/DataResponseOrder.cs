using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponseOrder
    {
        public string UserName { get; set; }
        public double? OriginalPrice { get; set; }
        public double? ActualPrice { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string address { get; set; }
        public string PaymentMethod { get; set; }
        public string StatusName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public IQueryable<DataResponseOrderDetail>? dataResponseOrderDetails { get; set; }
    }
}
