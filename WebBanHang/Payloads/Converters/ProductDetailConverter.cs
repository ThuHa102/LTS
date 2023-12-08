using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class ProductDetailConverter
    {
        private readonly AppDbContext context;
        public ProductDetailConverter()
        {
            context = new AppDbContext();
        }
        public DataResponseProductDetail ProductDetailToDTO(Product product)
        {
            return new DataResponseProductDetail()
            {
                ProductId = product.ProductId,
                NameProductType = context.ProductTypes.FirstOrDefault(x => x.ProductTypeId == product.ProductTypeId).NameProductType,
                NameProduct = product.NameProduct,
                Price = product.Price,
                AvataImageProduct = product.AvataImageProduct,
                Title = product.Title,
                Discount = product.Discount,
                Status = product.Status,
                NumberOfViews = product.NumberOfViews,
                NumberOfBuys = context.OrderDetails.Count(x => x.ProductId == product.ProductId),
            };
        }
    }
}
