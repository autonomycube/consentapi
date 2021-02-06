﻿using Contest.Wallet.Common.Data.UOW;
using Contest.Wallet.Common.Data.UOW.Abstract;
using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Utility.Services;
using Contest.Wallet.Api.Utility.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Utility.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFileUploadService, FileUploadService>();
        }
    }
}
