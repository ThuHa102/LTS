using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class Decentralization
    {
        [Key]
        public int DecentralizationId { get; set; }
        public string AuthorityName { get; set; }
        public DateTime? CreatetedAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}
