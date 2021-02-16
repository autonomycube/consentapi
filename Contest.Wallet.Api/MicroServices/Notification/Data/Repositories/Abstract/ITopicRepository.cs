using Consent.Common.Repository.SQL.Abstract;
using Consent.Common.EnityFramework.Entities;

namespace Consent.Api.Notification.Data.Repositories.Abstract
{
    public interface ITopicRepository : IRepository<TblNotifyTopic, string>
    {
    }
}
