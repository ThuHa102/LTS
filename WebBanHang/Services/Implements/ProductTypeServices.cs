using WebBanHang.Entities;
using WebBanHang.Payloads.Converters;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Services.Implements
{
    public class ProductTypeServices : BaseServices, IProductType
    {
        private readonly ResponseObject<DataResponseProductType> responseObject;
        private readonly ProductTypeConverter productTypeConverter;
        private readonly ResponseMessage responseMessage;
        public ProductTypeServices()
        {
            responseObject = new ResponseObject<DataResponseProductType>();
            productTypeConverter = new ProductTypeConverter();
            responseMessage = new ResponseMessage();
        }

        public ResponseObject<DataResponseProductType> ThemLoai(Request_AddProductType request)
        {
            if(context.ProductTypes.Any(x=> x.NameProductType.Equals(request.NameProductType)))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Loại sản phẩm này đã tồn tại !", null);
            }
            else
            {
                var productType = new ProductType();
                productType.NameProductType = request.NameProductType;
                productType.ImageTypeProduct = request.ImageTypeProduct;
                productType.CreatedAt = DateTime.Now;
                context.ProductTypes.Add(productType);
                context.SaveChanges();
                return responseObject.ThanhCong("Thêm loại sản phẩm thành công", productTypeConverter.ProductTypeToDTO(productType));
            }
        }
        public ResponseObject<DataResponseProductType> SuaLoai(ProductType productType)
        {
            if(!context.ProductTypes.Any(x=>x.ProductTypeId == productType.ProductTypeId))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Loai sản phẩm không tồn tại !", null);
            }
            else
            {
                var prType = context.ProductTypes.FirstOrDefault(x => x.ProductTypeId == productType.ProductTypeId);
                prType.NameProductType = productType.NameProductType;
                prType.ImageTypeProduct = productType.ImageTypeProduct;
                prType.UpdateAt = DateTime.Now;
                context.ProductTypes.Update(prType);
                context.SaveChanges();
                return responseObject.ThanhCong("Sửa thành công", productTypeConverter.ProductTypeToDTO(productType));
            }
        }

        public ResponseMessage XoaLoai(int productTypeId)
        {
            if(!context.ProductTypes.Any(x=>x.ProductTypeId == productTypeId))
            {
                return responseMessage.ThatBai("Loại sản phẩm không tồn tại !");
            }
            else
            {
                var productType = context.ProductTypes.FirstOrDefault(x=>x.ProductTypeId==productTypeId);
                var lstProduct = context.Products.ToList();
                foreach(var product in lstProduct)
                {
                    if(product.ProductTypeId == productTypeId)
                    {
                        var lstOrderDetail = context.OrderDetails.ToList();
                        foreach(var orderDetail in lstOrderDetail)
                        {
                            if(orderDetail.ProductId == product.ProductId)
                            {
                                context.OrderDetails.Remove(orderDetail);
                                context.SaveChanges();
                            }
                        }
                        var lstProductReview = context.ProductReviews.ToList();
                        foreach( var productReview in lstProductReview)
                        {
                            if(productReview.ProductId == product.ProductId)
                            {
                                context.ProductReviews.Remove(productReview);
                                context.SaveChanges();
                            }
                        }
                        context.Products.Remove(product);
                        context.SaveChanges();
                    }
                }
                context.ProductTypes.Remove(productType);
                context.SaveChanges();
                return responseMessage.ThanhCong("Xóa thành công");
            }
        }
    }
}
