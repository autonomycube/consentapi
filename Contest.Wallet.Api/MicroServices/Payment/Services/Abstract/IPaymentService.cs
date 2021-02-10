using System.Threading.Tasks;

namespace Consent.Api.Payment.Services.Abstract
{
    public interface IPaymentService
    {
        Task<string[]> Get();
    }
}
