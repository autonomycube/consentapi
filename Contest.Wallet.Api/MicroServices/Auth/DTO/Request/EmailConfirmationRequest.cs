using FluentValidation;

namespace Consent.Api.Auth.DTO.Request
{
    public class EmailConfirmationRequest
    {
        public string Email { get; set; }
    }

    public class EmailConfirmationRequestValidator : AbstractValidator<EmailConfirmationRequest>
    {
        public EmailConfirmationRequestValidator()
        {
            RuleFor(o => o.Email).NotEmpty().NotEmpty();
        }
    }
}
