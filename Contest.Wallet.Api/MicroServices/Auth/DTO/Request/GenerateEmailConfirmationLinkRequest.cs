using FluentValidation;

namespace Consent.Api.Auth.DTO.Request
{
    public class GenerateEmailConfirmationLinkRequest
    {
        public string UserId { get; set; }
    }

    public class GenerateEmailConfirmationLinkRequestValidator : AbstractValidator<GenerateEmailConfirmationLinkRequest>
    {
        public GenerateEmailConfirmationLinkRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().NotEmpty();
        }
    }
}
