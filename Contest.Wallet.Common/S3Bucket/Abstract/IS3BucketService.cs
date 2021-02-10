using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Consent.Common.S3Bucket.Abstract
{
    public interface IS3BucketService
    {
        string GetS3FileUrl(string fileName, string bucketName);
        Task UploadFile(IFormFile file, string fileName, string bucketName);
        Task<bool> DeleteFile(string fileName, string bucketName);
    }
}