using JustSaying.Models;
using System.Threading.Tasks;

namespace Consent.Common.EventCommunication.Abstract
{
    public interface IPublisher
    {
        Task Publish(Message message);
    }
}
