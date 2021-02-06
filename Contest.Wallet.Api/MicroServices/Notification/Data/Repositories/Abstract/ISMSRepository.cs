using Contest.Wallet.Common.Repository.SQL.Abstract;
using Contest.Wallet.Api.Notification.Data.Entities;

namespace Contest.Wallet.Api.Notification.Data.Repositories.Abstract
{
    public interface ISMSRepository : IRepository<TblSMS, string>
    {
    }
}
