using System.Collections.Generic;

namespace Consent.Api.Notification.DTO.Request
{
    public class CustomMultiEmailRequest
    {
        public List<string> EmailList { get; set; }
        public string mailSubject { get; set; }
        public string htmlText { get; set; }
        public List<string> attachmentPath { get; set; }
    }
}
