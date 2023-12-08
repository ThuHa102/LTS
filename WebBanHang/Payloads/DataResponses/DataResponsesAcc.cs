using WebBanHang.Entities;

namespace WebBanHang.Payloads.DataResponses
{
    public class DataResponsesAcc
    {
        public int AccountId { get; set; }
        public string AccName { get; set; }
        public string Avatar { get; set; }
        public string AuthorityName { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
