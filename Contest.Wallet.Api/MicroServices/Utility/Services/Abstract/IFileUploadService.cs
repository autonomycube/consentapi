using System.Threading.Tasks;

namespace Consent.Api.Utility.Services.Abstract
{
    public interface IFileUploadService
    {
        Task<string[]> Get();
    }
}
