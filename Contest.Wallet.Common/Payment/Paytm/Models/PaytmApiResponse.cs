namespace Consent.Common.Payment.Paytm.Models
{
    public class PaytmApiResponse<TBody>
    {
        public PaytmHeaderResponse Header { get; set; }
        public TBody Body { get; set; }
    }

    public class PaytmHeaderResponse
    {
        public string RequestId { get; set; }
        public string ResponseTimestamp { get; set; }
        public string Version { get; set; }
        public string ClientId { get; set; }
        public string Signature { get; set; }
    }
}
