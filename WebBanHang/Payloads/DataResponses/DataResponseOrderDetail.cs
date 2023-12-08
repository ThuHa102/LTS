using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponseOrderDetail
    {
        public string NameProduct { get; set; }
        public int Quantity { get; set; }
        public double? PriceTotal { get; set; }
    }
}
