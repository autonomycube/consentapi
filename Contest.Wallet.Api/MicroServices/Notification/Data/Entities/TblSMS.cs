using Consent.Common.Repository.SQL.Models;

namespace Consent.Api.Notification.Data.Entities
{
    public partial class TblSMS : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
