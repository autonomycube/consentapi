using Consent.Common.Repository.SQL.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace Consent.Common.EnityFramework.Entities
{
    public class TblNotifyEmailTemplate : EntityBase<string>
    {
        public TblNotifyEmailTemplate()
        {
            Id = Guid.NewGuid().ToString();
        }

        public virtual string Context { get; set; }
        public virtual string SubContext { get; set; }
        public virtual string MailSubject { get; set; }
        public virtual string HtmlText { get; set; }
        public virtual string ArbHtmlText { get; set; }
        public virtual string ArbMailSubject { get; set; }
        public virtual bool HasPlaceholder { get; set; }
        public virtual bool? IsActive { get; set; }
    }
}
