using Contest.Wallet.Common.Repository.SQL.Abstract;
using Contest.Wallet.Api.Tenant.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contest.Wallet.Api.Tenant.Data.DbContexts
{
    public class IssuerDbContext : DbContext, IDbContext
    {
        public DbSet<TblIssuers> Tests { get; set; }

        public IssuerDbContext(DbContextOptions<IssuerDbContext> options) : base(options)
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
            builder.Entity<TblIssuers>(entity =>
            {
                entity.ToTable("tbl_test");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Prop)
                    .HasColumnName("prop")
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TenantId)
                    .HasColumnName("tenant_id")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasColumnName("created_by")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasColumnType("datetime(6)");

                entity.Property(e => e.UpdatedBy)
                    .IsRequired()
                    .HasColumnName("updated_by")
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnName("updated_date")
                    .HasColumnType("datetime(6)");
            });
        }
    }
}
