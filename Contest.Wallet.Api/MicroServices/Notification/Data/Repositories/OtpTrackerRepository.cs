using Consent.Api.Notification.Data.DbContexts;
using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Common.Data.UOW.Abstract;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.Repository.SQL;

namespace Consent.Api.Notification.Data.Repositories
{
    public class OtpTrackerRepository : Repository<TblNotifyOtpTracker, string>, IOtpTrackerRepository
    {
        #region Private Variables

        private readonly NotificationDbContext _context;
        private readonly IUnitOfWork<NotificationDbContext> _uow;

        #endregion

        #region Constructors

        public OtpTrackerRepository(NotificationDbContext context, IUnitOfWork<NotificationDbContext> uow) : base(context)
        {
            _context = context;
            _uow = uow;
        }

        #endregion
    }
}
