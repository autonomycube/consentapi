﻿using Consent.Api.Auth.Data.DbContexts;
using Consent.Api.Notification.Data.DbContexts;
using Consent.Api.Payment.Data.DbContexts;
using Consent.Api.Tenant.Data.DbContexts;
using Consent.Common.Constants;
using Consent.Common.Data.Configuration;
using Consent.Common.EnityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Consent.Api.Infrastructure.Helpers
{
    public static class StartupHelpers
    {
        /// <summary>
        /// Register DbContext for Consent
        /// Configure the connection strings in AppSettings.json
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseProvider = configuration.GetSection(nameof(DatabaseProviderConfiguration)).Get<DatabaseProviderConfiguration>();
            var authDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.AuthConnectionStringKey);
            var notificationDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.NotificationConnectionStringKey);
            var paymentDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.PaymentConnectionStringKey);
            var tenantDbConnectionString = configuration.GetConnectionString(ConfigurationConsts.TenantConnectionStringKey);

            switch (databaseProvider.ProviderType)
            {
                case DatabaseProviderType.SqlServer:
                    services.AddDbContext<ConsentIdentityDbContext>(options => options.UseSqlServer(authDbConnectionString));
                    services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authDbConnectionString));
                    services.AddDbContext<NotificationDbContext>(options => options.UseSqlServer(notificationDbConnectionString));
                    services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(paymentDbConnectionString));
                    services.AddDbContext<TenantDbContext>(options => options.UseSqlServer(tenantDbConnectionString));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(databaseProvider.ProviderType), $@"The value needs to be one of {string.Join(", ", Enum.GetNames(typeof(DatabaseProviderType)))}.");
            }
        }

        /// <summary>
        /// Add services for authentication, including Identity model, IdentityServer4 and external providers
        /// </summary>
        /// <typeparam name="TIdentityDbContext">DbContext for Identity</typeparam>
        /// <typeparam name="TUserIdentity">User Identity class</typeparam>
        /// <typeparam name="TUserIdentityRole">User Identity Role class</typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAuthenticationServices<TIdentityDbContext, TUserIdentity, TUserIdentityRole>(this IServiceCollection services, IConfiguration configuration) where TIdentityDbContext : DbContext
            where TUserIdentity : class
            where TUserIdentityRole : class
        {
            services.AddIdentity<TUserIdentity, TUserIdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<TIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
