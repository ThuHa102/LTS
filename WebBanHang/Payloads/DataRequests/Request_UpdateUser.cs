namespace WebBanHang.Payloads.DataRequests
{
    public class Request_UpdateUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
