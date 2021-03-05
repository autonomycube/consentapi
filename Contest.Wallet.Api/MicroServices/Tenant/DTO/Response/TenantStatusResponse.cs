namespace Consent.Api.Tenant.DTO.Response
{
    public class TenantStatusResponse
    {
        public int RegistratedCount { get; set; }
        public int KycInProgressCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
    }
}
