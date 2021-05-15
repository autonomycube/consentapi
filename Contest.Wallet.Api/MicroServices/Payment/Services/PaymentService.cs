using AutoMapper;
using Consent.Api.MicroServices.Payment.DTO.Response;
using Consent.Api.Payment.Data.Repositories.Abstract;
using Consent.Api.Payment.Services.Abstract;
using Consent.Common.EnityFramework.Entities;
using Consent.Common.Helpers.Abstract;
using Consent.Common.Payment.Paytm.Services.Abstract;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Payment.Services
{
    public class PaymentService : IPaymentService
    {
        #region Private Variables

        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        private readonly IPaytmApiService _paytmApiService;
        private readonly IBaseAuthHelper _baseAuthHelper;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public PaymentService(IPaymentTransactionRepository paymentTransactionRepository,
            IPaytmApiService paytmApiService,
            IBaseAuthHelper baseAuthHelper,
            IMapper mapper)
        {
            _paymentTransactionRepository = paymentTransactionRepository
                ?? throw new ArgumentNullException(nameof(paymentTransactionRepository));
            _paytmApiService = paytmApiService
                ?? throw new ArgumentNullException(nameof(paytmApiService));
            _baseAuthHelper = baseAuthHelper
                ?? throw new ArgumentNullException(nameof(baseAuthHelper));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion

        #region Public Methods

        public async Task<PaymentConfirmationResponse> GetPaymentTransaction(string orderId)
        {
            var payment = (await _paymentTransactionRepository.FindBy(x => x.OrderId == orderId)).FirstOrDefault();
            return _mapper.Map<PaymentConfirmationResponse>(payment);
        }

        public async Task<string> GenerateCheckSum(string orderId, string customerId)
        {
            string checkSum = _paytmApiService.GenerateCheckSum(orderId);
            await _paymentTransactionRepository.Add(new TblPaymentTransactions
            {
                OrderId = orderId,
                CustomerId = customerId,
                CheckSum = checkSum,
                CreatedBy = _baseAuthHelper.GetUserId(),
                UpdatedBy = _baseAuthHelper.GetUserId()
            });

            return checkSum;
        }

        public async Task<string> UpdatePaymentStatus(string paytmResponse)
        {
            string orderId = null;
            string mid = null;
            string txnId = null;
            string bankTxnId = null;
            string txnAmount = null;
            string currency = null;
            string status = null;
            string respCode = null;
            string respMsg = null;
            string txnDate = null;
            string gatewayName = null;
            string bankName = null;
            string paymentMode = null;
            string checkSum = null;
            string[] responseParams = paytmResponse.Split('&');
            foreach (var param in responseParams)
            {
                string[] keyValues = param.Split('=');
                if (keyValues.Length > 1)
                {
                    switch (keyValues[0])
                    {
                        case "MID": mid = keyValues[1]; break;
                        case "TXNID": txnId = keyValues[1]; break;
                        case "ORDERID": orderId = keyValues[1]; break;
                        case "BANKTXNID": bankTxnId = keyValues[1]; break;
                        case "TXNAMOUNT": txnAmount = keyValues[1]; break;
                        case "CURRENCY": currency = keyValues[1]; break;
                        case "STATUS": status = keyValues[1]; break;
                        case "RESPCODE": respCode = keyValues[1]; break;
                        case "RESPMSG": respMsg = keyValues[1]; break;
                        case "TXNDATE": txnDate= keyValues[1]; break;
                        case "GATEWAYNAME":gatewayName = keyValues[1]; break;
                        case "BANKNAME": bankName = keyValues[1]; break;
                        case "PAYMENTMODE": paymentMode = keyValues[1]; break;
                        case "CHECKSUMHASH": checkSum = keyValues[1]; break;
                    }
                }
            }

            var payment = (await _paymentTransactionRepository.FindBy(x => x.OrderId == orderId)).FirstOrDefault();
            if (payment != null)
            {
                payment.MID = mid;
                payment.TransactionId = txnId;
                payment.BankTransactionId = bankTxnId;
                payment.TransactionAmount = txnAmount;
                payment.Currency = currency;
                payment.Status = status;
                payment.ResponseCode = respCode;
                payment.ResponseMessage = respMsg;
                payment.TransactionDate = string.IsNullOrEmpty(txnDate) ? (DateTime?)null : Convert.ToDateTime(txnDate);
                payment.GatewayName = gatewayName;
                payment.BankName = bankName;
                payment.PaymentMode = paymentMode;
                
                await _paymentTransactionRepository.Update(payment);
            }

            return orderId;
        }

        #endregion
    }
}
