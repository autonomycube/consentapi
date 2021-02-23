using System;

namespace Consent.Api.Tenant.DTO.Request
{
    public class TenantFilter
    {
        public string Name { get; set; }
        public string CIN { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
