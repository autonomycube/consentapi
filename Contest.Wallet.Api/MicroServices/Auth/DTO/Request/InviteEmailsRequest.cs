using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Auth.DTO.Request
{
    public class InviteEmailsRequest
    {
        public List<string> EmailList { get; set; }
    }

    public class InviteEmailsRequestValidator : AbstractValidator<InviteEmailsRequest>
    {
        public InviteEmailsRequestValidator()
        {
            RuleFor(o => o.EmailList).NotEmpty();
        }
    }
}
