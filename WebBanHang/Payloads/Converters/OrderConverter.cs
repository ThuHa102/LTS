using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class OrderConverter
    {
        private readonly AppDbContext context;
        private readonly OrderDetailConverter orderDetailConverter;
        public OrderConverter()
        {
            context = new AppDbContext();
            orderDetailConverter = new OrderDetailConverter();
        }
        public DataResponseOrder EnityToDTO(Order order)
        {
            return new DataResponseOrder()
            {
                UserName = context.Users.SingleOrDefault(x=>x.UserId == order.UserId).UserName,
                OriginalPrice = order.OriginalPrice,
                ActualPrice = order.ActualPrice,
                FullName = order.FullName,
                Email = order.Email,
                Phone = order.Phone,
                address = order.address,
                PaymentMethod = context.Payments.SingleOrDefault(x=>x.PaymentId == order.PaymentId).PaymentMethod,
                StatusName = context.OrderStatuses.SingleOrDefault(x=>x.OderStatusId == order.OderStatusId).StatusName,
                CreatedAt = order.CreatedAt,
                UpdateAt = order.UpdateAt,
                dataResponseOrderDetails = context.OrderDetails.Where(x=>x.OrderId == order.OrderId).Select(x=>orderDetailConverter.EntityToDTO(x))
            };
        }
    }
}
