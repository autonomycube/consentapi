using FluentValidation;

namespace Consent.Api.Auth.DTO.Request
{
    public class GenerateOtpRequest
    {
        public string UserId { get; set; }
    }

    public class GenerateOtpRequestValidator : AbstractValidator<GenerateOtpRequest>
    {
        public GenerateOtpRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().NotEmpty();
        }
    }
}
