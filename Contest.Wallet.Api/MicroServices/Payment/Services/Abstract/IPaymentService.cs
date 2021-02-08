using System.Threading.Tasks;

namespace Contest.Wallet.Api.Payment.Services.Abstract
{
    public interface IPaymentService
    {
        Task<string[]> Get();
    }
}
