using System.Collections.Generic;

namespace Consent.Api.Notification.DTO.Request
{
    public class TopicMessageRequest
    {
        public List<string> TenantList { get; set; }
        public string Message { get; set; }
    }
}
