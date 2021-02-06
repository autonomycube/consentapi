using Contest.Wallet.Common.Repository.SQL.Abstract;
using Contest.Wallet.Api.Tenant.Data.Entities;

namespace Contest.Wallet.Api.Tenant.Data.Repositories.Abstract
{
    public interface IIssuerRepository : IRepository<TblIssuers, string>
    {
    }
}
