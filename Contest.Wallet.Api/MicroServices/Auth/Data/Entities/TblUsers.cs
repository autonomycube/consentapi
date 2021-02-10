using Consent.Common.Repository.SQL.Models;

namespace Consent.Api.Auth.Data.Entities
{
    public partial class TblUsers : EntityBase<string>
    {
        public string Prop { get; set; }
    }
}
