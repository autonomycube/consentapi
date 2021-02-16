using FluentValidation;

namespace Consent.Api.Tenant.Services.DTO.Request
{
    public class UpdateTenantRequest
    {
        public string Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Contact { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual short EmployeesCount { get; set; }
    }

    public class UpdateTenantRequestValidator : AbstractValidator<UpdateTenantRequest>
    {
        public UpdateTenantRequestValidator()
        {
            RuleFor(o => o.Phone).NotNull().NotEmpty();
            RuleFor(o => o.EmployeesCount).NotNull();
        }
    }
}