namespace Consent.Api.Notification.DTO.Response
{
    public class GenerateOtpResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ReferenceId { get; set; }
    }
}
