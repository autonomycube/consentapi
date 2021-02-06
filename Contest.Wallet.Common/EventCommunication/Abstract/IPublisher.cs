using JustSaying.Models;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.EventCommunication.Abstract
{
    public interface IPublisher
    {
        Task Publish(Message message);
    }
}
