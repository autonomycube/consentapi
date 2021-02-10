using System.Threading.Tasks;

namespace Consent.Api.Auth.Services.Abstract
{
    public interface IUserService
    {
        Task<string[]> Get();
    }
}
