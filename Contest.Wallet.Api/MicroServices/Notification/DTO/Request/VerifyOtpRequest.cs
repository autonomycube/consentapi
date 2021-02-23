using FluentValidation;

namespace Consent.Api.Notification.DTO.Request
{
    public class VerifyOtpRequest
    {
        public string PhoneNumber { get; set; }
        public string Otp { get; set; }
        public string ReferenceId { get; set; }
    }

    public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequest>
    {
        public VerifyOtpRequestValidator()
        {
            RuleFor(o => o.PhoneNumber).NotEmpty().NotEmpty();
            RuleFor(o => o.Otp).NotEmpty().NotEmpty();
            RuleFor(o => o.ReferenceId).NotEmpty().NotEmpty();
        }
    }
}
