namespace Consent.Api.Tenant.Services.DTO.Request
{
    public class CreateTenantRequest
    {
        public string Contact { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public short EmployeesCount { get; set; }
        public string CIN { get; set; }
    }
}