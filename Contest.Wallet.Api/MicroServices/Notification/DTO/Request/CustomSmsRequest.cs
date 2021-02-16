using FluentValidation;

namespace Consent.Api.Notification.DTO.Request
{
    public class CustomSmsRequest
    {
        public string MobileNumber { get; set; }
        public string smsMessage { get; set; }
    }

    public class CustomSmsRequestValidator : AbstractValidator<CustomSmsRequest>
    {
        public CustomSmsRequestValidator()
        {
            RuleFor(o => o.MobileNumber).NotEmpty();
            RuleFor(o => o.smsMessage).NotEmpty();           
        }
    }
}
