using Contest.Wallet.Common.Repository.SQL.Models;

namespace Contest.Wallet.Api.Payment.Data.Entities
{
    public partial class TblPayments : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
