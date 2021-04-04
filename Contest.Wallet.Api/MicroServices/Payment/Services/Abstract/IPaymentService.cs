using Consent.Api.MicroServices.Payment.DTO.Response;
using System.Threading.Tasks;

namespace Consent.Api.Payment.Services.Abstract
{
    public interface IPaymentService
    {
        Task<PaymentConfirmationResponse> GetPaymentTransaction(string orderId);
        Task<string> GenerateCheckSum(string orderId, string customerId);
        Task<string> UpdatePaymentStatus(string paytmResponse);
    }
}
