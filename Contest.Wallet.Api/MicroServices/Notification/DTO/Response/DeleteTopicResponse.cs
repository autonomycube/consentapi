namespace Consent.Api.Notification.DTO.Response
{
    public class DeleteTopicResponse
    {
        public bool DeleteStatus { get; set; }
        public string ErrMessage { get; set; }
        public bool NoTopic { get; set; }
    }
}
