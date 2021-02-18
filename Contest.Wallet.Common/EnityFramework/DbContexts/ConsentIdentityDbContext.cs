using Consent.Common.EnityFramework.Constants;
using Consent.Common.EnityFramework.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Consent.Common.EnityFramework.DbContexts
{
    public class ConsentIdentityDbContext : IdentityDbContext<UserIdentity, UserIdentityRole, string>
    {
        public ConsentIdentityDbContext(DbContextOptions<ConsentIdentityDbContext> options) : base(options)
        {
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
            builder.Entity<UserIdentityRole>().ToTable(TableConsts.IdentityRoles);
            builder.Entity<UserIdentityUserRole>().ToTable(TableConsts.IdentityUserRoles);
            builder.Entity<UserPermissions>().ToTable(TableConsts.IdentityUserPermissions);
            builder.Entity<UserRolePermission>().ToTable(TableConsts.IdentityUserRolePermission);
        }
    }
}
