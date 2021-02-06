using Contest.Wallet.Common.Repository.SQL;
using Contest.Wallet.Api.Auth.Data.DbContexts;
using Contest.Wallet.Api.Auth.Data.Entities;
using Contest.Wallet.Api.Auth.Data.Repositories.Abstract;

namespace Contest.Wallet.Api.Auth.Data.Repositories
{
    public class UserRepository
        : Repository<TblUsers, string>, IUserRepository
    {
        public UserRepository(AuthDbContext context)
            : base(context)
        {
        }
    }
}
