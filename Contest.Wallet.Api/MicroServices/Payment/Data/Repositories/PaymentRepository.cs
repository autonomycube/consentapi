using Consent.Api.Payment.Data.DbContexts;
using Consent.Api.Payment.Data.Entities;
using Consent.Api.Payment.Data.Repositories.Abstract;
using Consent.Common.Repository.SQL;

namespace Consent.Api.Payment.Data.Repositories
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
