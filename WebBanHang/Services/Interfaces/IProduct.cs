using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IProduct
    {
        public IEnumerable<DataResponseProduct> GetAll (int pageSize, int pageNumber);
        ResponseObject<DataResponseProductDetail> XemChiTietSanPham(int productId);
        public IEnumerable<DataResponseProduct> HienThiSanPhamLienQuan (int productId, int pageSize, int pageNumber);
        public IEnumerable<DataResponseProduct> HienThiSanPhamNoiBat(int top, int pageSize, int pageNumber);
    }
}
