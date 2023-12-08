namespace WebBanHang.Payloads.DataRequests
{
    public class Request_ChangePassword
    {
        public string AccName { get; set; }
        public string ResetPasswordToken { get; set; }
        public string NewPassword { get; set; }
    }
}
