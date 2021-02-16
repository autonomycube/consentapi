namespace Consent.Api.Notification.DTO.Response
{
    public class CreateTmpEmailResponse
    {
        public bool MailStatus { get; set; }
        public string ErrMessage { get; set; }
        public bool NoTemplate { get; set; }
    }
}
