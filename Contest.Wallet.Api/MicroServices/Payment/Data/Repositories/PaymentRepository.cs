using Contest.Wallet.Api.Payment.Data.DbContexts;
using Contest.Wallet.Api.Payment.Data.Entities;
using Contest.Wallet.Api.Payment.Data.Repositories.Abstract;
using Contest.Wallet.Common.Repository.SQL;

namespace Contest.Wallet.Api.Payment.Data.Repositories
{
    public class PaymentRepository
        : Repository<TblPayments, string>, IPaymentRepository
    {
        public PaymentRepository(PaymentDbContext context)
            : base(context)
        {
        }
    }
}
