using FluentValidation;

namespace Consent.Api.Notification.DTO.Request
{
    public class VerifyOtpRequest
    {
        public string ReferenceId { get; set; }
        public string OTP { get; set; }
    }
    public class VerifyOtpRequestValidator : AbstractValidator<VerifyOtpRequest>
    {
        public VerifyOtpRequestValidator()
        {
            RuleFor(o => o.ReferenceId).NotEmpty();
            RuleFor(o => o.OTP).NotEmpty();
           
        }
    }
}
