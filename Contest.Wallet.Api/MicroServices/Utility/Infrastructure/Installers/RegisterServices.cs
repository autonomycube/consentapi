using Consent.Common.Data.UOW;
using Consent.Common.Data.UOW.Abstract;
using Consent.Api.Contracts;
using Consent.Api.Utility.Services;
using Consent.Api.Utility.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Utility.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFileUploadService, FileUploadService>();
        }
    }
}
