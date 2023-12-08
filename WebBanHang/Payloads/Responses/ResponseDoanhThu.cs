namespace WebBanHang.Payloads.Responses
{
    public class ResponseDoanhThu
    {
        public string Message { get; set; }
        public double? DoanhThu { get; set; }
        public ResponseDoanhThu() { }
        public ResponseDoanhThu(string message, double doanhthu)
        {
            Message = message;
            DoanhThu = doanhthu;
        }
        public ResponseDoanhThu(string message)
        {
            Message = message;
        }
        public ResponseDoanhThu ThanhCong (string message, double doanhthu)
        {
            return new ResponseDoanhThu(message, doanhthu);
        }
        public ResponseDoanhThu ThatBai (string message)
        {
            return new ResponseDoanhThu(message);
        }
    }
}
