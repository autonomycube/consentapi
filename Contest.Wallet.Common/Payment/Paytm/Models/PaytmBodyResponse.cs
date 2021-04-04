namespace Consent.Common.Payment.Paytm.Models
{
    public class PaytmBodyResponse
    {
        public PaytmResultInfoResponse ResultInfo { get; set; }
    }

    public class PaytmResultInfoResponse
    {
        public string ResultStatus { get; set; }
        public string ResultCode { get; set; }
        public string ResultMsg { get; set; }
    }
}
