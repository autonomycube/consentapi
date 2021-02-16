using System;

namespace Consent.Api.Tenant.Services.DTO.Response
{
    public class TenantOnboardCommentResponse
    {
        public string Id { get; set; }
        public string Comment { get; set; }
        public string TenantId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}