using Consent.Api.Payment.Data.DbContexts;
using Consent.Api.Payment.Data.Repositories.Abstract;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.Repository.SQL;

namespace Consent.Api.Payment.Data.Repositories
{
    public class PaymentTransactionRepository
        : Repository<TblPaymentTransactions, string>, IPaymentTransactionRepository
    {
        public PaymentTransactionRepository(PaymentDbContext context)
            : base(context)
        {
        }
    }
}
