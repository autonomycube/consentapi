using System;

namespace Consent.Api.MicroServices.Payment.DTO.Response
{
    public class PaymentConfirmationResponse
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string CheckSum { get; set; }
        public string MID { get; set; }
        public string TransactionId { get; set; }
        public string TransactionAmount { get; set; }
        public string Currency { get; set; }
        public string PaymentMode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string GatewayName { get; set; }
        public string BankTransactionId { get; set; }
        public string BankName { get; set; }
    }
}
