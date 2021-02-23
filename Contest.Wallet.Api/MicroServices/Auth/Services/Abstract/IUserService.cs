using Consent.Common.EnityFramework.Entities.Identity;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Services.Abstract
{
    public interface IUserService
    {
        UserIdentity GetById(string id);
        Task<UserIdentity> GetByPhoneNumber(string phoneNumber);
        Task<UserIdentity> GetByEmail(string email);
        Task Update(UserIdentity entity);
    }
}