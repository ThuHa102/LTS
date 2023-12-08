using WebBanHang.Entities;
using WebBanHang.Message;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IUser
    {
        public ErrorMessages AddUser(User user);
        ResponseObject<DataResponseUser> SuaThongTin(Request_UpdateUser request);
        public ErrorMessages DeleteUser(User user);
    }
}
