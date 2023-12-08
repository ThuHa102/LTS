using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class OrderStatus
    {
        [Key]
        public int OderStatusId { get; set; }
        public string StatusName { get; set; }
    }
}
