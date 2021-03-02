using Consent.Api.Notification.DTO.Response;
using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Tenant.Services.Abstract
{
    public interface IHolderService
    {
        IEnumerable<HolderEmailAddressesResponse> ValidateEmails(ValidateEmailAddressesRequest request);

        Task<IEnumerable<HolderEmailAddressesResponse>> SendEmailInvitations(HolderInviteEmailsRequest request);
    }
}
