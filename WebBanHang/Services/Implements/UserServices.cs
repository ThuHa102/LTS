using WebBanHang.Entities;
using WebBanHang.Message;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;
using WebBanHang.Services.Interfaces;

namespace WebBanHang.Services.Implements
{ 
    public class UserServices : IUser
    {
        private readonly AppDbContext context;
        public UserServices()
        {
            context = new AppDbContext();
        }
        public ErrorMessages AddUser(User user)
        {
            if(context.Users.Any(x=>x.UserName.Equals(user.UserName)))
            {
                return ErrorMessages.DaTonTai;
            }
            if(!context.Users.Any(x=>x.AccountId == user.AccountId))
            {
                return ErrorMessages.ChuaTonTai;
            }
            context.Users.Add(user);
            context.SaveChanges();
            return ErrorMessages.ThanhCong;
        }

        public ErrorMessages DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public ResponseObject<DataResponseUser> SuaThongTin(Request_UpdateUser request)
        {
            var user = context.Users.FirstOrDefault(x=>x.UserId == request.UserId);
            user.UserName = request.UserName;
            user.Address = request.Address;
            user.Phone = request.Phone;
            user.Email = request.Email;
            context.Users.Update(user); 
            throw new NotImplementedException();
        }
    }
}
