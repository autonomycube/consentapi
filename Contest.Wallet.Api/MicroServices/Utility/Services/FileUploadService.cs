using AutoMapper;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Api.Utility.Services.Abstract;
using System;
using System.Threading.Tasks;
using Consent.Common.S3Bucket.Abstract;
using Microsoft.Extensions.Configuration;
using Consent.Api.MicroServices.Utility.DTO.Response;
using Microsoft.AspNetCore.Http;

namespace Consent.Api.Utility.Services
{
    public class FileUploadService : IFileUploadService
    {
        #region Private Variables

        private readonly string _fileUploadBucket;
        private readonly IS3BucketService _s3BucketService;
        private readonly ILogger<FileUploadService> _logger;

        #endregion

        #region Constructor

        public FileUploadService(IS3BucketService s3BucketService,
            ILogger<FileUploadService> logger,
            IConfiguration configuration)
        {
            _s3BucketService = s3BucketService
                ?? throw new ArgumentNullException(nameof(s3BucketService));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _fileUploadBucket = configuration["S3Bucket"];
        }

        #endregion

        #region Public Methods

        public async Task<bool> DeleteS3file(string Key)
        {
            return await _s3BucketService.DeleteS3key(Key, _fileUploadBucket);
        }

        public async Task<FileUploadResponse> Upload(IFormFile file, string key)
        {
            try
            {
                string fileUrl = $"{key}/{file.FileName}";

                await _s3BucketService.UploadFile(file, fileUrl, _fileUploadBucket);
                return new FileUploadResponse()
                {
                    FileUrl = fileUrl
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Uploading S3 bucket - Exception: {0}", ex.Message);
                throw ex;
            }
        }


        #endregion

    }
}
