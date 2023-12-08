using WebBanHang.Entities;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IAccount
    {
        ResponseObject<DataResponsesAcc> Register(Request_Register request);
        DataResponseToken GenerateAccessToken(Account account);
        DataResponseToken RenewAccessToken(Request_RenewAccessToken request);
        ResponseObject<DataResponseToken> Login(Request_Login request);
        ResponseMessage ConfirmAccount(Request_ConfirmAcc request);
        ResponseMessage ForgotPassword(Request_ForgotPassword request);
        ResponseMessage ChangePassword (Request_ChangePassword request);
        //ResponseObject<DataResponsConfirmEmail> ConfirmEmail(Request_Register request);
    }
}
