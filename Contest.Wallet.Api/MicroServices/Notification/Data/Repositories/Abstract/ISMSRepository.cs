using Consent.Common.Repository.SQL.Abstract;
using Consent.Api.Notification.Data.Entities;

namespace Consent.Api.Notification.Data.Repositories.Abstract
{
    public interface ISMSRepository : IRepository<TblSMS, string>
    {
    }
}
