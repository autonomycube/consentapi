using Consent.Api.Notification.API.v1.DTO.Request;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface IEmailService
    {
        Task<bool> SendEmail(CreateEmailRequest request);
    }
}
