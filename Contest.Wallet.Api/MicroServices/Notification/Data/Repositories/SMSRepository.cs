using Contest.Wallet.Common.Repository.SQL;
using Contest.Wallet.Api.Notification.Data.DbContexts;
using Contest.Wallet.Api.Notification.Data.Entities;
using Contest.Wallet.Api.Notification.Data.Repositories.Abstract;

namespace Contest.Wallet.Api.Notification.Data.Repositories
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
