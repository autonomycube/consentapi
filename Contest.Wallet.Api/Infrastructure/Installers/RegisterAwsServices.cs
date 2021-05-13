using Amazon.Runtime;
using Amazon.S3;
using Consent.Api.Contracts;
using Consent.Common.S3Bucket;
using Consent.Common.S3Bucket.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Infrastructure.Installers
{
    public class RegisterAwsServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            string accessKey = configuration["AWS:AccessKey"];
            string secretKey = configuration["AWS:SecretKey"];
            services.AddSingleton(new AmazonS3Client(new BasicAWSCredentials(accessKey, secretKey)));
            services.AddSingleton<IS3BucketService, S3BucketService>();
        }
    }
}