using Consent.Api.Payment.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Payment.Data.Helpers
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
                await EnsureIssuerSeedData(services);
            }
        }

        public static async Task EnsureDatabasesMigrated(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = scope.ServiceProvider.GetRequiredService<PaymentDbContext>())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }

        public static async Task EnsureIssuerSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var testDBContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();

                await EnsureSeedIssuerData(testDBContext);
            }
        }

        /// <summary>
        /// Generate default Categories, Roles, Connection Types, Genders
        /// </summary>
        private static async Task EnsureSeedIssuerData(PaymentDbContext testDbContext)
        {
            if (!testDbContext.Tests.Any())
            {
            }
        }
    }
}