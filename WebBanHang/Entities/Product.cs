using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }
        public string NameProduct { get; set; }
        public double Price { get; set; }
        public string AvataImageProduct { get; set; }
        public string Title { get; set; }
        public int Discount { get; set; }
        public int? Status { get; set; }
        public int NumberOfViews { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
