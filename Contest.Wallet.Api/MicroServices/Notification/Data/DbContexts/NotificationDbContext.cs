using Consent.Common.EnityFramework.Constants;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.Repository.SQL.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace Consent.Api.Notification.Data.DbContexts
{
    public class NotificationDbContext : DbContext, IDbContext
    {
        public DbSet<TblNotifyEmailTemplate> EmailTemplates { get; set; }
        public DbSet<TblNotifySmsTemplate> SmsTemplates { get; set; }
        public DbSet<TblNotifyOtpTracker> OtpTrackers { get; set; }
        public DbSet<TblNotifyTopic> Topics { get; set; }
        public DbSet<TblNotifyUserSubscription> UserSubscriptions { get; set; }

        public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options)
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
            builder.Entity<TblNotifyEmailTemplate>().ToTable(TableConsts.NotifyEmailTemplate);
            builder.Entity<TblNotifySmsTemplate>().ToTable(TableConsts.NotifySmsTemplate);
            builder.Entity<TblNotifyOtpTracker>().ToTable(TableConsts.NotifyOtpTracker);
            builder.Entity<TblNotifyTopic>().ToTable(TableConsts.NotifyTopic);
            builder.Entity<TblNotifyUserSubscription>().ToTable(TableConsts.NotifyUserSubscription);
        }
    }
}
