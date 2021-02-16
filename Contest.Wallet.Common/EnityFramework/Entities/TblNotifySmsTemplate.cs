using Consent.Common.Repository.SQL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public class TblNotifySmsTemplate : EntityBase<string>
    {
        public TblNotifySmsTemplate()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Context { get; set; }
        public string SubContext { get; set; }
        public string SmsContent { get; set; }
        public string ArabicContent { get; set; }
        public virtual bool HasPlaceholder { get; set; }
        public virtual bool? IsActive { get; set; }
    }
}
