using System.Threading.Tasks;

namespace Contest.Wallet.Api.Notification.Services.Abstract
{
    public interface ISMSService
    {
        Task<string[]> Get();
    }
}
