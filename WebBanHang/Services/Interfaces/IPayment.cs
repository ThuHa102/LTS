using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;

namespace WebBanHang.Services.Interfaces
{
    public interface IPayment
    {
        string CreatePaymentUrl(Request_VNPayPayment model, HttpContext context);
        VnPayPaymentResponses PaymentExecute(IQueryCollection collections, Request_VNPayPayment request);
    }
}
