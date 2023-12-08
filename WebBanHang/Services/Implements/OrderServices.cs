using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Net.WebSockets;
using WebBanHang.Entities;
using WebBanHang.Payloads.Converters;
using WebBanHang.Payloads.DataRequests;
using WebBanHang.Payloads.DataResponses;
using WebBanHang.Payloads.Responses;
using WebBanHang.Services.Interfaces;
using WebBanHang.Handle.Email;

namespace WebBanHang.Services.Implements
{
    public class OrderServices : BaseServices, IOrder
    {
        private readonly ResponseObject<DataResponseOrder> responseObject;
        private readonly OrderConverter orderConverter;
        private readonly ResponseDoanhThu responseDoanhThu;
        private readonly ResponseMessage responseMessage;
        public OrderServices()
        {
            responseObject = new ResponseObject<DataResponseOrder>();
            orderConverter = new OrderConverter();
            responseDoanhThu = new ResponseDoanhThu();
            responseMessage = new ResponseMessage();
        }

        public ResponseObject<DataResponseOrder> AddOrder( Request_AddOrder request)
        {
            var user = context.Users.SingleOrDefault(x=>x.UserId == request.UserId);
            if(user == null)
            {
                return responseObject.ThatBai(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", null);
            }
            if (!Validate.IsValidEmail(request.Email))
            {
                return responseObject.ThatBai(StatusCodes.Status400BadRequest, "Dinh dang Email khong hop le", null);
            }
            Order order = new Order();
            order.UserId = user.UserId;
            order.FullName = request.FullName;
            order.Email = request.Email;
            order.Phone = request.Phone;
            order.address = request.address;
            order.PaymentId = request.PaymentId;
            order.OderStatusId = 1;
            order.OriginalPrice = 0;
            order.ActualPrice = 0;
            order.CreatedAt = DateTime.Now;
            order.UpdateAt = DateTime.Now;
            context.Orders.Add(order);
            context.SaveChanges();
            order.OrderDetails = AddListOrderDetail(order.OrderId, request.request_AddOrderDetails);
            context.Orders.Update(order);
            context.SaveChanges();
            double orPrice = 0;
            double acPrice = 0;
            foreach (var item in order.OrderDetails)
            {
                var sp = context.Products.SingleOrDefault(x=>x.ProductId ==  item.ProductId);
                orPrice += sp.Price * item.Quantity;
                acPrice += item.PriceTotal;
            }
            order.OriginalPrice = orPrice;
            order.ActualPrice = acPrice;
            context.Orders.Update(order);
            context.SaveChanges();
            return responseObject.ThanhCong("Thêm hóa đơn thành công", orderConverter.EnityToDTO(order));
        }

        private List<OrderDetail> AddListOrderDetail(int orderId, List<Request_AddOrderDetail> request)
        {
            var hoadon = context.Orders.SingleOrDefault(x => x.OrderId == orderId);
            if(hoadon is null)
            {
                return null;
            }
            List<OrderDetail> list = new List<OrderDetail>();
            foreach(var rq in request)
            {
                OrderDetail ct = new OrderDetail();
                var sp = context.Products.SingleOrDefault(x => x.ProductId == rq.ProductId);
                if(sp is null)
                {
                    throw new Exception("Sản phẩm không tồn tại");
                }
                ct.OrderId = orderId;
                ct.ProductId = rq.ProductId;
                ct.Quantity = rq.Quantity;
                ct.PriceTotal = (sp.Price - sp.Discount) * rq.Quantity;
                list.Add(ct);
            }
            context.OrderDetails.AddRange(list);
            context.SaveChanges();
            return list;
        }
        public ResponseDoanhThu TinhDoanhThuThang(int thang, int nam)
        {
            double dt = 0;
            if(thang >12 || thang < 1)
            {
                return responseDoanhThu.ThatBai("Tháng không hợp lệ !");
            }
            else if(nam <0 || nam > DateTime.Now.Year)
            {
                return responseDoanhThu.ThatBai("Năm không hợp lệ !");
            }
            else
            {
                 var doanhthu = context.Orders
                .Where(x=>x.CreatedAt.Value.Month == thang && x.CreatedAt.Value.Year == nam)
                .Sum(x=>x.ActualPrice);
                dt += (double)doanhthu;
                return responseDoanhThu.ThanhCong($"Doanh thu thang {thang}/{nam}: ", dt);
            }
        }

        public ResponseDoanhThu TinhDoanhThuQuy(int quy, int nam)
        {
            double dt = 0;
            if (quy > 4 || quy < 1)
            {
                return responseDoanhThu.ThatBai("Quý nhập không hợp lệ !");
            }
            else if (nam < 0 || nam > DateTime.Now.Year)
            {
                return responseDoanhThu.ThatBai("Năm không hợp lệ !");
            }
            DateTime startDate = new DateTime(nam, (quy - 1) * 3 + 1, 1);
            DateTime endDate = startDate.AddMonths(3).AddDays(-1);

            var doanhthu = context.Orders
                .Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
                .Sum(x=>x.ActualPrice);
            dt += (double)doanhthu;
            return responseDoanhThu.ThanhCong($"Doanh thu quy {quy}/{nam}: ", dt);
        }

        public ResponseDoanhThu TinhDoanhThuNam(int nam)
        {
            double dt = 0;
            var list = context.Orders.ToList();
            if (nam <0 || nam > DateTime.Now.Year)
            {
                return responseDoanhThu.ThatBai("Năm không hợp lệ!");
            }
            else
            {
                var doanhthu = context.Orders
                 .Where(x => x.CreatedAt.Value.Year == nam)
                 .Sum(x => x.ActualPrice);
                dt += (double)doanhthu;
                return responseDoanhThu.ThanhCong($"Doanh thu nam {nam}: ", dt);
            }
        }

        public List<DataResponseOrder> GetAll(int pageSize, int pageNumber)
        {
            var list = context.Orders.Skip(pageNumber -1).Take(pageSize).Select(x=>orderConverter.EnityToDTO(x)).ToList();
            return list;
        }

        //public ResponseMessage CapNhatTrangThaiDonHang(int orderId, int orderStatusId)
        //{
        //    var order = context.Orders.SingleOrDefault(x=>x.OrderId == orderId);
        //    //var user = context.Users.SingleOrDefault(x => x.UserId == order.UserId);
        //    //var acc = context.Accounts.SingleOrDefault(x => x.AccountId == user.AccountId);
        //    string email = order.Email;
        //    order.OderStatusId = orderStatusId;
        //    context.Orders.Update(order);
        //    context.SaveChanges();
        //    var smtpClient = new SmtpClient("smtp.gmail.com")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential("dangthuha01072k2@gmail.com", "cwku bvbd euzo qpzk"),
        //        EnableSsl = true
        //    };
        //    try
        //    {
        //        var message = new MailMessage();
        //        message.From = new MailAddress("dangthuha01072k2@gmail.com");
        //        message.To.Add(email);
        //        message.Subject = "Cập nhật trạng thái đơn hàng";
        //        var ordStatus = context.OrderStatuses.FirstOrDefault(x => x.OderStatusId == orderStatusId);
        //        message.Body = $"Đơn hàng {orderId} {ordStatus.StatusName}";
        //        message.IsBodyHtml = true;
        //        smtpClient.Send(message);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return responseMessage.ThanhCong("Cập nhật trạng thái đơn hàng thành công");
        //}
    }
}
