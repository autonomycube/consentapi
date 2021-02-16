using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface IEmailService
    {
        Task<CreateTmpEmailResponse> SendSingleTemplateEmail(CreateTmpEmailRequest emailRequest);
        Task<bool> SendCustomEmail(CustomEmailRequest customRequest);
        Task<CreateTmpEmailResponse> SendMultipleTemplatedEmail(MultipleTmpEmailRequest emailRequest);
        Task<bool> SendMultipleCustomEmail(CustomMultiEmailRequest customRequest);
    }
}
