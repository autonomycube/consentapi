using Consent.Common.EnityFramework.Entities;
using System.Collections.Generic;

namespace Consent.Api.Data.Configuration
{
    public class NotificationSeedData
    {
        public List<TblNotifySmsTemplate> SmsTemplates { get; set; }
        public List<TblNotifyEmailTemplate> EmailTemplates { get; set; }
    }
}
