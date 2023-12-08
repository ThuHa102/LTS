using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class OrderDetailConverter
    {
        private readonly AppDbContext context;
        public OrderDetailConverter()
        {
            context = new AppDbContext();
        }
        public DataResponseOrderDetail EntityToDTO (OrderDetail orderDetail)
        {
            return new DataResponseOrderDetail()
            {
                NameProduct = context.Products.FirstOrDefault(x => x.ProductId == orderDetail.ProductId).NameProduct,
                Quantity = orderDetail.Quantity,
                PriceTotal = orderDetail.PriceTotal,
            };
        }
    }
}
