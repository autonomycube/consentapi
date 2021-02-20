using Consent.Common.EnityFramework.Entities.Identity;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Data.Repositories.Abstract
{
    public interface IUserRepository
    {
        UserIdentity GetById(string id);
        Task Update(UserIdentity entity);
    }
}
