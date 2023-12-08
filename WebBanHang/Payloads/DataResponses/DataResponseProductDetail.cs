using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponseProductDetail
    {
        public int ProductId { get; set; }
        public string NameProductType { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public string AvataImageProduct { get; set; }
        public string Title { get; set; }
        public int Discount { get; set; }
        public int? Status { get; set; }
        public int NumberOfViews { get; set; }
        public int NumberOfBuys { get; set; }
    }
}
