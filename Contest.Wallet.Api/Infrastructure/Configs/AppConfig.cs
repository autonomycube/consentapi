namespace Consent.Api.Infrastructure.Configs
{
    public class AppConfig
    {        
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string AwsS3Bucket { get; set; }
        public string FromMail { get; set; }        
        public string ForgotLink { get; set; }
        public string EmailLink { get; set; }
        public string SenderID { get; set; }
        public string AuthUrl { get; set; }
    }
}
