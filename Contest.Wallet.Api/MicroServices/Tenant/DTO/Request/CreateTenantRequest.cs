using FluentValidation;

namespace Consent.Api.Tenant.Services.DTO.Request
{
    public class CreateTenantRequest
    {
        public string CIN { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public short EmployeesCount { get; set; }
    }

    public class CreateTenantRequestValidator : AbstractValidator<CreateTenantRequest>
    {
        public CreateTenantRequestValidator()
        {
            RuleFor(o => o.CIN).NotNull().NotEmpty();
            RuleFor(o => o.PhoneNumber).NotNull().NotEmpty();
            RuleFor(o => o.Email).NotNull().NotEmpty()
                .EmailAddress().WithMessage("Email is invalid");
            RuleFor(o => o.EmployeesCount).NotNull();
        }
    }
}