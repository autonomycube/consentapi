using Consent.Api.MicroServices.Utility.DTO.Response;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Consent.Api.Utility.Services.Abstract
{
    public interface IFileUploadService
    {
        Task<bool> DeleteS3file(string key);
        Task<FileUploadResponse> Upload(IFormFile file, string key);
    }
}
