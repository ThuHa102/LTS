using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public int Status { get; set; }
        public DateTime? CretaedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
