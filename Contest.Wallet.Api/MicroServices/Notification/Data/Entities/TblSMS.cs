using Contest.Wallet.Common.Repository.SQL.Models;

namespace Contest.Wallet.Api.Notification.Data.Entities
{
    public partial class TblSMS : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
