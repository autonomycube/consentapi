using Contest.Wallet.Common.Repository.SQL.Models;

namespace Contest.Wallet.Api.Auth.Data.Entities
{
    public partial class TblUsers : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
