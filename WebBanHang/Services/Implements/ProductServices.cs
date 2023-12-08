using WebBanHang.Entities;
using WebBanHang.Payloads.Converters;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Services.Implements
{
    public class ProductServices : BaseServices, IProduct
    {
        private readonly ProductConverter productConverter;
        private readonly ProductDetailConverter productDetailConverter;
        private readonly ResponseObject<DataResponseProductDetail> responseObject;
        public ProductServices()
        {
            productConverter = new ProductConverter();
            productDetailConverter = new ProductDetailConverter();
            responseObject = new ResponseObject<DataResponseProductDetail>();

        }
        public IEnumerable<DataResponseProduct> GetAll(int pageSize, int pageNumber)
        {
            var listsp = context.Products.Skip(pageNumber - 1).Take(pageSize).Select(x=> productConverter.ProductToDTO(x)).ToList();
            return listsp;
        }

        public ResponseObject<DataResponseProductDetail> XemChiTietSanPham(int productId)
        {
            if(!context.Products.Any(x=>x.ProductId == productId))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Sản phẩm không tồn tại", null);
            }
            else
            {
                var sp = context.Products.FirstOrDefault(x => x.ProductId == productId);
                return responseObject.ThanhCong($"Chi tiết sản phẩm {sp.NameProduct}: ", productDetailConverter.ProductDetailToDTO(sp));
            }
        }
        public IEnumerable<DataResponseProduct> HienThiSanPhamLienQuan(int productId, int pageSize, int pageNumber)
        {
            var sp = context.Products.FirstOrDefault(x => x.ProductId == productId);
            var lst = context.Products.Where(x=>x.ProductTypeId == sp.ProductTypeId).Skip(pageNumber -1).Take(pageSize).Select(x=>productConverter.ProductToDTO(x)).ToList();
            return lst;
        }

        public List<DataResponseProduct> SelectTop(int top)
        {
            var lstTop = context.Products.OrderByDescending(x=>x.NumberOfViews).Take(top).Select(x=>productConverter.ProductToDTO(x)).ToList();
            return lstTop;
        }
        public IEnumerable<DataResponseProduct> HienThiSanPhamNoiBat(int top, int pageSize, int pageNumber)
        {
            var lstNoiBat = SelectTop(top).Skip(pageNumber - 1).Take(pageSize).ToList();
            return lstNoiBat;
        }

    }
}
