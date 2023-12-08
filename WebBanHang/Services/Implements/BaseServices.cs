using WebBanHang.Entities;

namespace WebBanHang.Services.Implements
{
    public class BaseServices
    {
        public readonly AppDbContext context;
        public BaseServices()
        {
            context = new AppDbContext();
        }
    }
}
