using Consent.Common.EnityFramework.Entities;
using FluentValidation;

namespace Consent.Api.Tenant.DTO.Request
{
    public class CreateTenantOnboardCommentRequest
    {
        public string Comment { get; set; }
        public string TenantId { get; set; }
        public TenantStatus Status { get; set; }
    }

    public class CreateTenantOnboardCommentRequestValidator : AbstractValidator<CreateTenantOnboardCommentRequest>
    {
        public CreateTenantOnboardCommentRequestValidator()
        {
            RuleFor(o => o.TenantId).NotNull().NotEmpty();
            RuleFor(o => o.Comment).NotNull().NotEmpty();
            RuleFor(o => o.Status).NotNull();
        }
    }
}