using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }
        public string NameProductType { get; set; }
        public string ImageTypeProduct { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
