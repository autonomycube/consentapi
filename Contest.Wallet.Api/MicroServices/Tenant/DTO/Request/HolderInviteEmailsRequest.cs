using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Tenant.DTO.Request
{
    public class HolderInviteEmailsRequest
    {
        public List<string> Emails { get; set; }
    }

    public class HolderInviteEmailsRequestValidator : AbstractValidator<HolderInviteEmailsRequest>
    {
        public HolderInviteEmailsRequestValidator()
        {
            RuleFor(o => o.Emails).NotNull();
            When(o => o.Emails?.Count > 0, () =>
            {
                RuleForEach(o => o.Emails).EmailAddress().WithMessage("Email address is not valid");
            });
        }
    }
}
