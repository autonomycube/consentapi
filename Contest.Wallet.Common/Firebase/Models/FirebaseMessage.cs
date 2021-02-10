using System.Collections.Generic;

namespace Consent.Common.Firebase.Models
{
    public class FirebaseMessage
    {
        public string Title { get; set; }
        public string Token { get; set; }
        public string Topic { get; set; }
        public string Body { get; set; }
        public IDictionary<string, string> Data { get; set; }
    }
}