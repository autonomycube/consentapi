using Contest.Wallet.Api.Payment.Data.Entities;
using Contest.Wallet.Common.Repository.SQL.Abstract;

namespace Contest.Wallet.Api.Payment.Data.Repositories.Abstract
{
    public interface IPaymentRepository : IRepository<TblPayments, string>
    {
    }
}
