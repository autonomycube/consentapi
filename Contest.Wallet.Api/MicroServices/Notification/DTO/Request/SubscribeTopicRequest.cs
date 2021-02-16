using FluentValidation;

namespace Consent.Api.Notification.DTO.Request
{
    public class SubscribeTopicRequest
    {
        public string TenantId { get; set; }
        public string TenantName { get; set; }
        public string MobileNumber { get; set; }
    }

    public class SubscribeTopicRequestValidator : AbstractValidator<SubscribeTopicRequest>
    {
        public SubscribeTopicRequestValidator()
        {
            RuleFor(o => o.TenantId).NotEmpty();
            RuleFor(o => o.TenantName).NotEmpty();
            RuleFor(o => o.MobileNumber).NotEmpty();
        }
    }
}
