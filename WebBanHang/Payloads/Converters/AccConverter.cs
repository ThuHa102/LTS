using WebBanHang.Entities;
using WebBanHang.Payloads.DataResponses;

namespace WebBanHang.Payloads.Converters
{
    public class AccConverter
    {
        private readonly AppDbContext context;
        public AccConverter()
        {
            context = new AppDbContext();
        }
        public DataResponsesAcc EntityToDTO(Account account)
        {
            return new DataResponsesAcc
            {
                AccountId = account.AccountId,
                AccName = account.AccName,
                Avatar = account.Avatar,
                AuthorityName = context.Decentralizations.FirstOrDefault(x => x.DecentralizationId == account.DecentralizationId).AuthorityName,
                CreatedAt = account.CreatedAt
                
            };
        }
    }
}
