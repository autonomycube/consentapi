using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Tenant.DTO.Request
{
    public class ValidateEmailAddressesRequest
    {
        public List<string> Emails { get; set; }
    }

    public class ValidateEmailAddressesRequestValidator : AbstractValidator<ValidateEmailAddressesRequest>
    {
        public ValidateEmailAddressesRequestValidator()
        {
            RuleFor(o => o.Emails).NotNull();
            When(o => o.Emails?.Count > 0, () =>
            {
                RuleForEach(o => o.Emails).EmailAddress().WithMessage("Email address is not valid");
            });
        }
    }
}