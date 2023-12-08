using WebBanHang.Entities;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IProductType
    {
        ResponseObject<DataResponseProductType> ThemLoai(Request_AddProductType request);
        ResponseObject<DataResponseProductType> SuaLoai(ProductType productType);
        ResponseMessage XoaLoai(int productTypeId);
    }
}
