using FluentValidation;

namespace Consent.Api.Auth.DTO.Request
{
    public class VerifyOtpRequest
    {
        public string UserId { get; set; }
        public string Otp { get; set; }
        public string ReferenceId { get; set; }
    }

    public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequest>
    {
        public VerifyOtpRequestValidator()
        {
            RuleFor(o => o.UserId).NotEmpty().NotEmpty();
            RuleFor(o => o.Otp).NotEmpty().NotEmpty();
            RuleFor(o => o.ReferenceId).NotEmpty().NotEmpty();
        }
    }
}
