using System.Threading.Tasks;

namespace Contest.Wallet.Api.Auth.Services.Abstract
{
    public interface IUserService
    {
        Task<string[]> Get();
    }
}
