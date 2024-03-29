﻿using Consent.Common.Repository.SQL.Abstract;
using Consent.Api.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace Consent.Api.Auth.Data.DbContexts
{
    public class AuthDbContext : DbContext, IDbContext
    {
        public DbSet<TblUsers> Tests { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
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
            builder.Entity<TblUsers>(entity =>
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
