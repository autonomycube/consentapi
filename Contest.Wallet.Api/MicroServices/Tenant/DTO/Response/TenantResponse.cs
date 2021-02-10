namespace Consent.Api.Tenant.Services.DTO.Response
{
    public class TenantResponse
    {
        public string Id { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public short EmployeesCount { get; set; }
        public string CIN { get; set; }
    }
}