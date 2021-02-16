using FluentValidation;

namespace Consent.Api.Tenant.Services.DTO.Request
{
    public class CreateTenantRequest
    {
        public virtual string CIN { get; set; }
        public virtual string Name { get; set; }
        public virtual string Address { get; set; }
        public virtual string Contact { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual short EmployeesCount { get; set; }
    }

    public class CreateTenantRequestValidator : AbstractValidator<CreateTenantRequest>
    {
        public CreateTenantRequestValidator()
        {
            RuleFor(o => o.CIN).NotNull().NotEmpty();
            RuleFor(o => o.Phone).NotNull().NotEmpty();
            RuleFor(o => o.EmployeesCount).NotNull();
        }
    }
}