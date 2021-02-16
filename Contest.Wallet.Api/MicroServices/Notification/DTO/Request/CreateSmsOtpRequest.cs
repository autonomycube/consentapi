using FluentValidation;

namespace Consent.Api.Notification.DTO.Request
{
    public class CreateSmsOtpRequest
    {
        public string MobileNumber { get; set; }
    }

    public class CreateSmsOtpRequestValidator : AbstractValidator<CreateSmsOtpRequest>
    {
        public CreateSmsOtpRequestValidator()
        {
            RuleFor(o => o.MobileNumber).NotEmpty();
        }
    }
}