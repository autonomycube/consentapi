using FluentValidation;

namespace Consent.Api.Notification.API.v1.DTO.Request
{
    public class VerifySmsOtpRequest
    {
        public string MobileNumber { get; set; }
        public string OTP { get; set; }
        public string ReferenceId { get; set; }
    }

    public class VerifySmsOtpRequestValidator : AbstractValidator<VerifySmsOtpRequest>
    {
        public VerifySmsOtpRequestValidator()
        {
            RuleFor(o => o.MobileNumber).NotNull().NotEmpty();
            RuleFor(o => o.OTP).NotNull().NotEmpty();
            RuleFor(o => o.ReferenceId).NotNull().NotEmpty();
        }
    }
}