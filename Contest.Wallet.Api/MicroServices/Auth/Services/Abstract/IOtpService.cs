using Consent.Api.Auth.DTO.Request;
using Consent.Api.Auth.DTO.Response;
using System.Threading.Tasks;

namespace Consent.Api.Auth.Services.Abstract
{
    public interface IOtpService
    {
        Task<GenerateOtpResponse> GeneratePhoneNumberConfirmationOtp(GenerateOtpRequest request);
        Task<VerifyOtpResponse> VerifyPhoneNumberConfirmationOtp(VerifyOtpRequest request);
    }
}
