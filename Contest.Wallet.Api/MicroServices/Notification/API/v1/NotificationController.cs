using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Consent.Api.Notification.API.v1
{
    [Produces("application/json")]
    [Route("api/v1/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        #region Private Variables

        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly ILogger<NotificationController> _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public NotificationController(ISmsService smsService,
            IEmailService emailService,
            IMapper mapper,
            ILogger<NotificationController> logger)
        {
            _smsService = smsService
                ?? throw new ArgumentNullException(nameof(smsService));
            _emailService = emailService
                ?? throw new ArgumentNullException(nameof(emailService));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Sms

        /// <summary>
        /// send templated sms to single endpoint.
        /// </summary>
        /// <param name="smsRequest">sms request parameters</param>
        /// <returns></returns>
        [HttpPost("sms/single/template")]
        public async Task<ApiResponse> SendTemplateSms([FromBody] CreateTmpSmsRequest smsRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _smsService.SendSingleTemplateSMS(smsRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))//error
                    throw new ApiException(result.ErrMessage, result.NoTemplate ? Status404NotFound : Status500InternalServerError);
                else
                    return new ApiResponse("Sms successfully sent.", result.OutputMessage, Status200OK);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Send custom sms to single endpoint
        /// </summary>
        /// <param name="customRequest">custom sms parameters</param>
        /// <returns></returns>
        [HttpPost("sms/single/custom")]
        public async Task<ApiResponse> SendCustomSms([FromBody] CustomSmsRequest customRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _smsService.SendCustomSMS(customRequest);
                if (result)
                    return new ApiResponse("Sms successfully sent.", string.Empty, Status200OK);
                else
                    throw new ApiException("Sms sending failed.", Status500InternalServerError);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Verifies the otp send via sms
        /// </summary>
        /// <param name="otpRequest">otp verification parameters</param>
        /// <returns></returns>
        [HttpPost("sms/verifyotp")]
        public async Task<ApiResponse> VerifyOTP([FromBody] VerifyOtpRequest otpRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _smsService.VerifyOtp(otpRequest);
                if (result.IsValid)
                    return new ApiResponse("Otp successfully verified.", result, Status200OK);
                else
                {
                    if (result.IsError)
                        throw new ApiException("Internal server Error.", Status500InternalServerError);
                    else
                        throw new ApiException("Invalid otp.", Status400BadRequest);
                }
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Subscribe an endpoint to topic
        /// </summary>
        /// <param name="topicRequest">endpoint and topic details</param>
        /// <returns></returns>
        [HttpPost("sms/topic/subscribe")]
        public async Task<ApiResponse> SubscribeToTopic([FromBody] SubscribeTopicRequest topicRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var response = await _smsService.subscribeEndpointToTopic(topicRequest);
                if (response)
                    return new ApiResponse("Subscribed successfully.", response, Status200OK);
                else
                    throw new ApiException("Subscribing failed.", Status500InternalServerError);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Publish message to topic
        /// </summary>
        /// <param name="messageRequest"></param>
        /// <returns></returns>
        [HttpPost("sms/topic/publishMessage")]
        public async Task<ApiResponse> PublishMessage([FromBody] TopicMessageRequest messageRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var response = await _smsService.PublishMessageToTopic(messageRequest);
                if (response)
                    return new ApiResponse("Message successfully sent.", true, Status200OK);
                else
                    throw new ApiException("Message sending failed.", Status500InternalServerError);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Unsubscribe endpoint from topic
        /// </summary>
        /// <param name="unsubscribeRequest"></param>
        /// <returns></returns>
        [HttpPost("sms/topic/unsubscribe")]
        public async Task<ApiResponse> UnSubscribeToTopic([FromBody] UnsubscribeTopicRequest unsubscribeRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _smsService.UnsubscribeEndpointToTopic(unsubscribeRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))//error
                    throw new ApiException(result.ErrMessage, result.NoArn ? Status404NotFound : Status500InternalServerError);
                else
                    return new ApiResponse("Unsubscribed successfully.", result.Status, Status200OK);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        /// <summary>
        /// Delete specific topic
        /// </summary>
        /// <param name="TenantId"></param>
        /// <returns></returns>
        [HttpDelete("sms/topic/{TenantId}/delete")]
        public async Task<ApiResponse> DeleteTopic(string TenantId)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _smsService.DeleteTopic(TenantId);
                if (!string.IsNullOrEmpty(result.ErrMessage))//error
                    throw new ApiException(result.ErrMessage, result.NoTopic ? Status404NotFound : Status500InternalServerError);
                else
                    return new ApiResponse("Deleted successfully.", result.DeleteStatus, Status200OK);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        #endregion

        #region Email

        [HttpPost("email/single/template")]
        public async Task<ApiResponse> SendTemplateEmail([FromBody] CreateTmpEmailRequest emailRequest)
        {
            if (ModelState.IsValid)
            {
                //call service              
                var result = await _emailService.SendSingleTemplateEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))//error
                    throw new ApiException(result.ErrMessage, result.NoTemplate ? Status404NotFound : Status500InternalServerError);
                else
                    return new ApiResponse("Email successfully sent.", result.MailStatus, Status200OK);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        [HttpPost("email/single/custom")]
        public async Task<ApiResponse> SendCustomEmail([FromBody] CustomEmailRequest emailRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _emailService.SendCustomEmail(emailRequest);
                if (result)
                    return new ApiResponse("Email successfully sent.", string.Empty, Status200OK);
                else
                    throw new ApiException("Email sending failed.", Status500InternalServerError);

            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        [HttpPost("email/multiple/template")]
        public async Task<ApiResponse> SendMultipleTemplateEmail([FromBody] MultipleTmpEmailRequest emailRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _emailService.SendMultipleTemplatedEmail(emailRequest);
                if (!string.IsNullOrEmpty(result.ErrMessage))//error
                    throw new ApiException(result.ErrMessage, result.NoTemplate ? Status404NotFound : Status500InternalServerError);
                else
                    return new ApiResponse("Email successfully sent.", result.MailStatus, Status200OK);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        [HttpPost("email/multiple/custom")]
        public async Task<ApiResponse> SendMultipleCustomEmail([FromBody] CustomMultiEmailRequest customRequest)
        {
            if (ModelState.IsValid)
            {
                //call service
                var result = await _emailService.SendMultipleCustomEmail(customRequest);
                if (result)
                    return new ApiResponse("Email successfully sent.", string.Empty, Status200OK);
                else
                    throw new ApiException("Email sending failed.", Status500InternalServerError);
            }
            else
                throw new ApiException(ModelState.AllErrors());
        }

        #endregion
    }
}