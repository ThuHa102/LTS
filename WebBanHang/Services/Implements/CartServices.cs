using System.Net.WebSockets;
using WebBanHang.Entities;
using WebBanHang.Message;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Services.Implements
{
    public class CartServices : BaseServices, ICart
    {
        public ErrorMessages AddCart(Carts carts)
        {
            if(context.Users.Any(x=>x.UserId == carts.UserId))
            {
                context.Carts.Add(carts);
                context.SaveChanges();
                return ErrorMessages.ThanhCong;
            }
            else
            {
                return ErrorMessages.ChuaTonTai;
            }
        }

        public ErrorMessages DeleteCart(int cartId)
        {
            if(context.Carts.Any(x=>x.CartId == cartId))
            {
                var cart = context.Carts.FirstOrDefault(x=>x.CartId==cartId);
                var lstCartItem = context.CartItems.ToList();
                foreach(var item in lstCartItem)
                {
                    if(item.CartId == cartId)
                    {
                        context.CartItems.Remove(item);
                        context.SaveChanges();
                    }
                }
                context.Carts.Remove(cart);
                context.SaveChanges();
                return ErrorMessages.ThanhCong;
            }
            else
            {
                return ErrorMessages.ChuaTonTai;
            }
        }

        public ErrorMessages UpdateCart(Carts carts)
        {
            if(context.Carts.Any(x=>x.CartId == carts.CartId))
            {
                if(context.Users.Any(x=>x.UserId == carts.UserId))
                {
                    context.Carts.Update(carts);
                    context.SaveChanges();
                    return ErrorMessages.ThanhCong;
                }
                return ErrorMessages.ChuaTonTai1;
            }
            return ErrorMessages.ChuaTonTai2;
        }
    }
}
