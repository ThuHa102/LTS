namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponseUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
