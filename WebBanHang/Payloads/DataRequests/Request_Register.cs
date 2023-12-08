using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataRequests
{
    public class Request_Register
    {
        public string AccName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
