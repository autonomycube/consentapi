﻿using Contest.Wallet.Common.Data.UOW;
using Contest.Wallet.Common.Data.UOW.Abstract;
using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Payment.Data.DbContexts;
using Contest.Wallet.Api.Payment.Services;
using Contest.Wallet.Api.Payment.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contest.Wallet.Api.Payment.Infrastructure.Installers
{
    public class RegisterServices : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUnitOfWork<PaymentDbContext>, UnitOfWork<PaymentDbContext>>();
            services.AddTransient<IPaymentService, PaymentService>();
        }
    }
}