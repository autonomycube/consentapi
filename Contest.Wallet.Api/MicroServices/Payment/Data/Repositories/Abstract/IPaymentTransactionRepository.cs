using Consent.Common.EnityFramework.Entities;
using Consent.Common.Repository.SQL.Abstract;

namespace Consent.Api.Payment.Data.Repositories.Abstract
{
    public interface IPaymentTransactionRepository : IRepository<TblPaymentTransactions, string>
    {
    }
}
