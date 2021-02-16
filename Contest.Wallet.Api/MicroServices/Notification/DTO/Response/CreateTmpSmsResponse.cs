namespace Consent.Api.Notification.DTO.Response
{
    public class CreateTmpSmsResponse
    {
        public string OutputMessage { get; set; }
        public string ErrMessage { get; set; }
        public bool NoTemplate { get; set; }
    }
}
