namespace WebBanHang.Payloads.Responses
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public ResponseMessage() { }
        public ResponseMessage(string message) 
        {
            Message = message;
        }
        public ResponseMessage ThanhCong(string message)
        {
            return new ResponseMessage(message);
        }
        public ResponseMessage ThatBai(string message)
        {
            return new ResponseMessage(message);
        }
    }
}
