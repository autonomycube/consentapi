using System.Threading.Tasks;

namespace Contest.Wallet.Api.Utility.Services.Abstract
{
    public interface IFileUploadService
    {
        Task<string[]> Get();
    }
}
