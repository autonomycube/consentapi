using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Api.Tenant.DTO.Response;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.Repository.SQL;
using System.Collections.Generic;
using System.Linq;

namespace Consent.Api.Tenant.Data.Repositories
{
    public class InvitationsRepository
        : Repository<TblTenantInvitations, string>, IInvitationsRepository
    {
        #region Private Variables

        private readonly TenantDbContext _context;

        #endregion

        #region Constructor

        public InvitationsRepository(TenantDbContext context)
            : base(context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public IEnumerable<HolderEmailAddressesResponse> ValidateEmails(List<string> emails)
        {
            return (from email in emails
                    join invitation in _context.Invitations on email equals invitation.Email into invitationsSent
                    from inviationSent in invitationsSent.DefaultIfEmpty()
                    join users in _context.Users on email equals users.Email into usersRegistered
                    from userRegistered in usersRegistered.DefaultIfEmpty()
                    select new HolderEmailAddressesResponse
                    {
                        Email = email,
                        Status = (userRegistered != null ? EnumHolderEmailAddressStatus.AlreadyRegistered : (inviationSent != null ? EnumHolderEmailAddressStatus.InvitationSent : EnumHolderEmailAddressStatus.Valid))
                    });
        }

        #endregion
    }
}
