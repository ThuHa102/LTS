using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int CartId { get; set; }
        public Carts? Carts { get; set; }
        public int Quantity { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
