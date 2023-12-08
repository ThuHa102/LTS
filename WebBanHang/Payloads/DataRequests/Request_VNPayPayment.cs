namespace WebBanHang.Payloads.DataRequests
{
    public class Request_VNPayPayment
    {
        public int OrderId { get; set; }
        public string OrderType { get; set; }
        //public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
    }
}
