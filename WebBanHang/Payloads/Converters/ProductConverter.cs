using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class ProductConverter
    {
        private readonly AppDbContext context;
        public ProductConverter()
        {
            context = new AppDbContext();
        }
        public DataResponseProduct ProductToDTO (Product product)
        {
            return new DataResponseProduct()
            {
                NameProduct = product.NameProduct,
                Price = product.Price,
                AvataImageProduct = product.AvataImageProduct,
                Discount = product.Discount,
                NumberOfViews = product.NumberOfViews,
                NumberOfBuys = context.OrderDetails.Count(x=>x.ProductId == product.ProductId),
                Status = product.Status,
            };
        }
    }
}
