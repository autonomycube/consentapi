using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using Consent.Common.EnityFramework.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface INotificationService
    {
        Task<GenerateOtpResponse> SendConfirmationOtp(UserIdentity user);
        Task<SuccessResponse> VerifyConfirmationOtp(VerifyOtpRequest request, UserIdentity user);
        Task<SuccessResponse> SendRegistrationEmail(UserIdentity user);
        Task<SuccessResponse> SendTenantOnboardCompletedEmail(string email);
        Task<SuccessResponse> SendTenantOnboardRejectedEmail(string email, string firstName, string reason);
        Task<SuccessResponse> SendEmailConfirmLink(string email, string link);
        Task<SuccessResponse> SendEmailInvitations(List<string> emails);
    }
}
