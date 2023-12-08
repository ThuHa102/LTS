using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class Carts
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
