using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class ProductTypeConverter
    {
        public DataResponseProductType ProductTypeToDTO(ProductType productType)
        {
            return new DataResponseProductType()
            {
                ProductTypeId = productType.ProductTypeId,
                NameProductType = productType.NameProductType,
                ImageTypeProduct = productType.ImageTypeProduct
            };
        }
    }
}
