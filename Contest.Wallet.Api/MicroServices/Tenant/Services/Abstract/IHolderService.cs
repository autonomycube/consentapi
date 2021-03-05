using Consent.Api.Tenant.DTO.Request;
using Consent.Api.Tenant.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Tenant.Services.Abstract
{
    public interface IHolderService
    {
        Task<HolderResponse> CreateHolder(CreateHolderRequest request);
        Task UpdateHolderKYE(List<string> emails);
        IEnumerable<HolderEmailAddressesResponse> ValidateEmails(EmailAddressesRequest request);
        Task<IEnumerable<HolderEmailAddressesResponse>> SendEmailInvitations(EmailAddressesRequest request);
    }
}
