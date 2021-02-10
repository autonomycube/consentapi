using Consent.Common.Firebase.Models;
using System.Threading.Tasks;

namespace Consent.Common.Firebase.Abstract
{
    public interface IFirebaseService
    {
        Task SendNotification(FirebaseMessage message);
    }
}