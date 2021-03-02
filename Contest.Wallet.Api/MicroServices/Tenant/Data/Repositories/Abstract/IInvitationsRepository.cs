using Consent.Common.Repository.SQL.Abstract;
using Consent.Common.EnityFramework.Entities;
using Consent.Api.Tenant.DTO.Response;
using System.Collections.Generic;

namespace Consent.Api.Tenant.Data.Repositories.Abstract
{
    public interface IInvitationsRepository : IRepository<TblTenantInvitations, string>
    {
        IEnumerable<HolderEmailAddressesResponse> ValidateEmails(List<string> emails);
    }
}
