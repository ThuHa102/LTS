using WebBanHang.Entities;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IOrder
    {
        ResponseObject<DataResponseOrder> AddOrder(Request_AddOrder request);
        public ResponseDoanhThu TinhDoanhThuThang(int thang, int nam);
        public ResponseDoanhThu TinhDoanhThuQuy(int quy, int nam);
        public ResponseDoanhThu TinhDoanhThuNam(int nam);
        public List<DataResponseOrder> GetAll(int pageSize, int pageNumber);
        //public ResponseMessage CapNhatTrangThaiDonHang(int orderId, int orderStatusId);
    }
}
