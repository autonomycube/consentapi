using Consent.Common.EnityFramework.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        UserIdentity GetById(string id);
        Task<IEnumerable<UserIdentity>> FindBy(Expression<Func<UserIdentity, bool>> predicate);
        Task Update(UserIdentity entity);
    }
}
