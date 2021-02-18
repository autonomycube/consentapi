using Consent.Api.Data.Configuration;
using Consent.Common.EnityFramework.Constants;
using Consent.Api.Notification.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Data.Helpers
{
    public static class DbMigrationHelpers
    {
        /// <summary>
        /// Generate migrations before running this method, you can use these steps bellow:
        /// https://github.com/skoruba/IdentityServer4.Admin#ef-core--data-access
        /// </summary>
        /// <param name="host"></param>      
        public static async Task EnsureSeedData(IHost host)
        {
            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;
                await EnsureDatabasesMigrated(services);
                await EnsureNotificationSeedData(services);
            }
        }

        public static async Task EnsureDatabasesMigrated(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<NotificationDbContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }

        public static async Task EnsureNotificationSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var notificationDBContext = scope.ServiceProvider.GetRequiredService<NotificationDbContext>();
                var notificationData = scope.ServiceProvider.GetRequiredService<IOptions<NotificationSeedData>>().Value;

                await EnsureSeedNotificationData(notificationDBContext, notificationData);
            }
        }

        private static async Task EnsureSeedNotificationData(NotificationDbContext context, NotificationSeedData notificationData)
        {
            if (!context.SmsTemplates.Any() && notificationData.SmsTemplates != null)
            {
                foreach (var template in notificationData.SmsTemplates)
                {
                    if (string.IsNullOrEmpty(template.ArabicContent)) template.ArabicContent = null;
                    template.IsActive = true;
                    template.CreatedBy = ConsentConsts.TenantId;
                    template.CreatedDate = DateTime.UtcNow;
                    template.UpdatedBy = ConsentConsts.TenantId;
                    template.UpdatedDate = DateTime.UtcNow;
                    context.SmsTemplates.Add(template);
                    await context.SaveChangesAsync();
                }
            }

            if (!context.EmailTemplates.Any() && notificationData.EmailTemplates != null)
            {
                foreach (var template in notificationData.EmailTemplates)
                {
                    template.IsActive = true;
                    template.CreatedBy = ConsentConsts.TenantId;
                    template.CreatedDate = DateTime.UtcNow;
                    template.UpdatedBy = ConsentConsts.TenantId;
                    template.UpdatedDate = DateTime.UtcNow;
                    context.EmailTemplates.Add(template);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}