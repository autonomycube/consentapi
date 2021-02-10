using Consent.Common.Repository.SQL;
using Consent.Api.Notification.Data.DbContexts;
using Consent.Api.Notification.Data.Entities;
using Consent.Api.Notification.Data.Repositories.Abstract;

namespace Consent.Api.Notification.Data.Repositories
{
    public class SMSRepository
        : Repository<TblSMS, string>, ISMSRepository
    {
        public SMSRepository(NotificationDbContext context)
            : base(context)
        {
        }
    }
}
