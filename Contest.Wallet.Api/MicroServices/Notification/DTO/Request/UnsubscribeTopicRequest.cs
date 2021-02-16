using FluentValidation;

namespace Consent.Api.Notification.DTO.Request
{
    public class UnsubscribeTopicRequest
    {
        public string TenantId { get; set; }
        public string MobileNumber { get; set; }
    }
    public class UnsubscribeTopicRequestValidator : AbstractValidator<UnsubscribeTopicRequest>
    {
        public UnsubscribeTopicRequestValidator()
        {
            RuleFor(o => o.TenantId).NotEmpty();
            RuleFor(o => o.MobileNumber).NotEmpty();            
        }
    }
}
