namespace Consent.Api.Notification.DTO.Response
{
    public class UnSubscribeTopicResponse
    {
        public bool Status { get; set; }
        public string ErrMessage { get; set; }
        public bool NoArn { get; set; }
    }
}
