using WebBanHang.Entities;
using WebBanHang.Message;

namespace WebBanHang.Services.Interfaces
{
    public interface ICart
    {
        public ErrorMessages AddCart(Carts carts);
        public ErrorMessages UpdateCart(Carts carts);
        public ErrorMessages DeleteCart(int cartId);
    }
}
