using Contest.Wallet.Common.Repository.SQL.Abstract;
using Contest.Wallet.Api.Auth.Data.Entities;

namespace Contest.Wallet.Api.Auth.Data.Repositories.Abstract
{
    public interface IUserRepository : IRepository<TblUsers, string>
    {
    }
}
