using Consent.Common.Repository.SQL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public class TblNotifyOtpTracker : EntityBase<string>
    {
        public TblNotifyOtpTracker()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string Context { get; set; }
        public virtual string ContextId { get; set; }
        public virtual string Otp { get; set; }
        public virtual bool OtpVerified { get; set; }
        public virtual OtpTrackerCategory Category { get; set; } = OtpTrackerCategory.Sms;
        public virtual bool? IsActive { get; set; }
    }

    public enum OtpTrackerCategory
    {
        Sms = 0,
        Email = 1
    }
}
