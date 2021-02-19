namespace Consent.Api.Auth.DTO.Request
{
    public class VerifyOtpRequest
    {
        public string UserId { get; set; }
        public string Otp { get; set; }
        public string ReferenceId { get; set; }
    }
}
