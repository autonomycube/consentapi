using Consent.Api.Tenant.Data.DbContexts;
using Consent.Api.Tenant.Data.Repositories.Abstract;
using Consent.Api.Tenant.DTO.Response;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;
using Consent.Common.Repository.SQL;
using System.Linq;

namespace Consent.Api.Tenant.Data.Repositories
{
    public class TenantRepository
        : Repository<TblAuthTenants, string>, ITenantRepository
    {
        #region Private Variables

        private readonly TenantDbContext _context;

        #endregion

        #region Constructor

        public TenantRepository(TenantDbContext context)
            : base(context)
        {
            _context = context;
        }

        #endregion

        #region Public Methods

        public HoldersStatusResponse GetHoldersStatusCount(string tenantId)
        {
            var users = (from user in _context.Users
                         where user.TenantId == tenantId && user.UserType == UserType.Holder
                         select new
                         {
                             IsKYE = user.IsKYE,
                             Registered = !user.IsKYE,
                             Invitaion = false,
                         })
                        .Union
                        (from invitation in _context.Invitations
                         where invitation.TenantId == tenantId
                         select new
                         {
                             IsKYE = false,
                             Registered = false,
                             Invitaion = !invitation.Registered
                         });

            return new HoldersStatusResponse
            {
                InvitionSentCount = users.Where(u => u.Invitaion).Count(),
                KYECompletedCount = users.Where(u => u.IsKYE).Count(),
                RegisteredCount = users.Where(u => u.Registered).Count()
            };
        }

        #endregion
    }
}
