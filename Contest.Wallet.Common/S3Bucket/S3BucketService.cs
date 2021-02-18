using Amazon.S3;
using Amazon.S3.Model;
using Consent.Common.S3Bucket.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Consent.Common.S3Bucket
{
    public class S3BucketService : IS3BucketService
    {
        #region Private Variables

        private readonly AmazonS3Client _amazonS3;

        #endregion

        #region Constructor

        public S3BucketService(AmazonS3Client amazonS3)
        {
            _amazonS3 = amazonS3
                ?? throw new ArgumentNullException(nameof(amazonS3));
        }

        #endregion

        #region Public Methods

        public async Task<bool> DeleteS3key(string S3Key, string bucketName)
        {
            var s3DeleteObjRequest = new DeleteObjectRequest();
            s3DeleteObjRequest.BucketName = bucketName;
            s3DeleteObjRequest.Key = S3Key;
            try
            {
                var response = await _amazonS3.DeleteObjectAsync(s3DeleteObjRequest);

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    throw new Exception($"Failed to delete file {S3Key} from bucket {bucketName}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Unable to Delete Key {0}", S3Key));
            }
        }

        public string GetS3FileUrl(string fileName, string bucketName)
        {
            return string.Format("https://{0}.s3.{1}.amazonaws.com/{2}", bucketName, _amazonS3.Config.RegionEndpoint.SystemName, fileName);
        }

        public async Task UploadFile(IFormFile file, string fileName, string bucketName)
        {
            // get the file and convert it to the byte[]
            byte[] fileBytes = new Byte[file.Length];
            try
            {
                file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Failed to read file {0}, error: {1}", fileName, ex.Message));
            }

            try
            {
                PutObjectResponse response = null;
                using (var stream = new MemoryStream(fileBytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucketName,
                        Key = fileName,
                        InputStream = stream,
                        ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead
                    };

                    response = await _amazonS3.PutObjectAsync(request);
                };

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                }
                else
                {
                    throw new Exception(string.Format("Failed to upload file {0} to S3 bucket {1}", fileName, bucketName));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteFile(string fileName, string bucketName)
        {
            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileName
                };

                var response = await _amazonS3.DeleteObjectAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    throw new Exception(string.Format("Failed to delete file {0} from bucket {1}", fileName, bucketName));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
