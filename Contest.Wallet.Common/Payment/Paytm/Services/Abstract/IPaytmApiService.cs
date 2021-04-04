using System.Threading.Tasks;

namespace Consent.Common.Payment.Paytm.Services.Abstract
{
    public interface IPaytmApiService
    {
        string GenerateCheckSum(string orderId);
    }
}
