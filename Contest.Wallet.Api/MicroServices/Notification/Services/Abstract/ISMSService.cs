using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface ISMSService
    {
        Task<string[]> Get();
    }
}
