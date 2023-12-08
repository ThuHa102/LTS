using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public Payment? Payment { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public double? OriginalPrice { get; set; }
        public double? ActualPrice { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string address { get; set; }
        public int OderStatusId { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public IEnumerable<OrderDetail>? OrderDetails { get; set; }
    }
}
