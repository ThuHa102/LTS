using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Entities
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        public string AccName { get; set; }
        public string? Avatar { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? ConfirmAcc { get; set; }
        public int? status { get; set; }
        public int DecentralizationId { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public Decentralization? Decentralization { get; set; }
    }
}
