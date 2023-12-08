namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponseProduct
    {
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public string AvataImageProduct { get; set; }
        public int Discount { get; set; }
        public int NumberOfViews { get; set; }
        public int NumberOfBuys { get; set; }
        public int? Status { get; set; }
    }
}
