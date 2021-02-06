using Contest.Wallet.Common.Firebase.Models;
using System.Threading.Tasks;

namespace Contest.Wallet.Common.Firebase.Abstract
{
    public interface IFirebaseService
    {
        Task SendNotification(FirebaseMessage message);
    }
}