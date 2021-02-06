using System.Threading.Tasks;

namespace Contest.Wallet.Api.Tenant.Services.Abstract
{
    public interface ITestService
    {
        Task<string[]> Get();
    }
}
