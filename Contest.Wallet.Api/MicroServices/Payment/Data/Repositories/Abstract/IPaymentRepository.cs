using Consent.Api.Payment.Data.Entities;
using Consent.Common.Repository.SQL.Abstract;

namespace Consent.Api.Payment.Data.Repositories.Abstract
{
    public interface IPaymentRepository : IRepository<TblPayments, string>
    {
    }
}
