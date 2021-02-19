using Consent.Common.EnityFramework.Entities.Identity;
using System;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        UserIdentity GetById(Guid uid);
        Task Update(UserIdentity entity);
    }
}
