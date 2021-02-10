using Consent.Common.Repository.SQL;
using Consent.Api.Auth.Data.DbContexts;
using Consent.Api.Auth.Data.Entities;
using Consent.Api.Auth.Data.Repositories.Abstract;

namespace Consent.Api.Auth.Data.Repositories
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
