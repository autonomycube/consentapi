using Consent.Common.Repository.SQL.Models;

namespace Consent.Api.Payment.Data.Entities
{
    public partial class TblPayments : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
