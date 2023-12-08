using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class DoanhThuConverter
    {
        private readonly AppDbContext context;
        public DoanhThuConverter()
        {
            context = new AppDbContext();
        }
        public DataRespondThongKeDoanhThu EntityToDTO(List<Order> lst)
        {
            return new DataRespondThongKeDoanhThu()
            {
                lstDoanhThu = lst,
            };
        }
    }
}
