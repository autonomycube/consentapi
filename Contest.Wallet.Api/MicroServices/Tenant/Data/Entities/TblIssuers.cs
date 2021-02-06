using Contest.Wallet.Common.Repository.SQL.Models;

namespace Contest.Wallet.Api.Tenant.Data.Entities
{
    public partial class TblIssuers : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
