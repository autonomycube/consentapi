using Consent.Common.Repository.SQL.Abstract;
using Consent.Api.Auth.Data.Entities;

namespace Consent.Api.Auth.Data.Repositories.Abstract
{
    public interface IUserRepository : IRepository<TblUsers, string>
    {
    }
}
