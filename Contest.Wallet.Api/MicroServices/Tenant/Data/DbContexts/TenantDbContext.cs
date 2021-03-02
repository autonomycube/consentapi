using Consent.Common.EnityFramework.Constants;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.EnityFramework.Entities.Identity;
using Consent.Common.Repository.SQL.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace Consent.Api.Tenant.Data.DbContexts
{
    public class TenantDbContext : DbContext, IDbContext
    {
        public DbSet<UserIdentity> Users { get; set; }
        public DbSet<TblAuthTenants> Tenants { get; set; }
        public DbSet<TblAuthTenantOnboardStatus> TenantOnboardStatus { get; set; }
        public DbSet<TblTenantInvitations> Invitations { get; set; }

        public TenantDbContext(DbContextOptions<TenantDbContext> options) : base(options)
        {
        }

        public DbQuery<TQuery> Query<TQuery>() where TQuery : class
        {
            throw new System.NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ConfigureIdentityContext(builder);
        }

        private void ConfigureIdentityContext(ModelBuilder builder)
        {
            builder.Entity<UserIdentity>().ToTable(TableConsts.IdentityUsers);
            builder.Entity<TblAuthTenants>().ToTable(TableConsts.IdentityUserTenants);
            builder.Entity<TblAuthTenantOnboardStatus>().ToTable(TableConsts.TenantOnboardStatus);
            builder.Entity<TblTenantInvitations>().ToTable(TableConsts.TenantInvitations);
        }
    }
}
