using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using Consent.Api.Notification.Services.Abstract;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.EnityFramework.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services
{
    public class SmsService : ISmsService
    {
        #region Private variables 

        private const string otpContext = "Send OTP";
        private readonly ILogger<SmsService> _logger;
        private readonly IAwsService _awsService;
        private readonly ISmsRepository _smsRepository;
        private readonly IOtpTrackerRepository _otpTrackerRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IUserSubscriptionRepository _subscriptionRepository;

        #endregion

        #region Constructor

        public SmsService(ILogger<SmsService> logger,
            IAwsService awsService,
            ISmsRepository smsRepository,
            IOtpTrackerRepository otpTrackerRepository,
            ITopicRepository topicRepository,
            IUserSubscriptionRepository subscriptionRepository,
            IConfiguration config)
        {
            _logger = logger;
            _awsService = awsService;
            _smsRepository = smsRepository;
            _otpTrackerRepository = otpTrackerRepository;
            _topicRepository = topicRepository;
            _subscriptionRepository = subscriptionRepository;
        }

        #endregion

        #region Public Methods

        public async Task<CreateTmpSmsResponse> SendSingleTemplateSMS(CreateTmpSmsRequest smsRequest)
        {
            CreateTmpSmsResponse smsResponse = new CreateTmpSmsResponse();
            smsResponse.OutputMessage = string.Empty;
            smsResponse.ErrMessage = string.Empty;
            string smsContent = string.Empty;
            try
            {
                //Get sms template from DB based on context & subcontext.
                TblNotifySmsTemplate smsTemplate = (await _smsRepository.Find(st => st.Context.Trim().ToLower().Equals(smsRequest.Context.Trim().ToLower()) &&
                                                      st.SubContext.Trim().ToLower().Equals(smsRequest.SubContext.Trim().ToLower()))).SingleOrDefault();

                if (smsTemplate == null)
                {
                    smsResponse.ErrMessage = "No Template found";
                    smsResponse.NoTemplate = true;
                    return smsResponse;
                }

                //Check for language                
                smsContent = smsRequest.IsArabic ? smsTemplate.ArabicContent : smsTemplate.SmsContent;
                if (string.IsNullOrEmpty(smsContent))
                {
                    smsResponse.ErrMessage = "No Template found";
                    smsResponse.NoTemplate = true;
                    return smsResponse;
                }

                if (smsTemplate.SubContext.Trim().ToLower().Equals(otpContext.Trim().ToLower()))
                {
                    var smsText = SetOTPMessage(smsContent, smsRequest.PlaceHolders);
                    var response = await _awsService.SendSms(smsRequest.MobileNumber, smsText.Item1);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        //Insert otp generated context to otptracker
                        smsResponse.OutputMessage = await LogOtp(smsRequest.Context, smsRequest.ContextId, smsText.Item2);
                    }
                }
                else
                {
                    //all other cases                     
                    if (smsRequest.PlaceHolders != null && smsRequest.PlaceHolders.Count > 0)
                    {
                        //with placeholders
                        smsContent = Api.Infrastructure.Extensions.StringExtensions.Format(smsTemplate.SmsContent, smsRequest.PlaceHolders);
                    }
                    else
                    {
                        //without placeholders
                        smsContent = smsTemplate.SmsContent;
                    }
                    var response = await _awsService.SendSms(smsRequest.MobileNumber, smsContent);
                }
            }
            catch (Exception ex)
            {
                smsResponse.ErrMessage = "Internal server error";
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return smsResponse;
        }

        public async Task<bool> SendCustomSMS(CustomSmsRequest smsRequest)
        {
            bool status = false;
            try
            {
                var response = await _awsService.SendSms(smsRequest.MobileNumber, smsRequest.smsMessage);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    status = true;
            }
            catch (Exception ex)
            {
                status = false;
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return status;
        }

        public async Task<VerifyOtpResponse> VerifyOtp(VerifyOtpRequest otpRequest)
        {
            VerifyOtpResponse otpResponse = new VerifyOtpResponse();
            otpResponse.IsValid = false;
            try
            {
                TblNotifyOtpTracker otpDetails = await _otpTrackerRepository.GetById(otpRequest.ReferenceId);
                if (otpDetails == null)
                {
                    otpResponse.IsValid = false;
                    return otpResponse;
                }

                if (otpDetails.Otp.Trim().Equals(otpRequest.Otp.Trim()))
                {
                    otpDetails.OtpVerified = true;
                    await _otpTrackerRepository.Update(otpDetails);
                    otpResponse.IsValid = true;
                }
            }
            catch (Exception ex)
            {
                otpResponse.IsError = true;
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return otpResponse;
        }

        public async Task<bool> SubscribeEndpointToTopic(SubscribeTopicRequest topicRequest)
        {
            string topicArn = string.Empty;
            try
            {
                //Check database if any topic is already there for the Tenant. If not create topic.
                TblNotifyTopic topic = (await _topicRepository.Find(t => t.TenantId == topicRequest.TenantId)).SingleOrDefault();
                //Create Topic and insert details to DB
                if (topic == null)
                {
                    topicArn = await _awsService.CreateSNSTopic(topicRequest.TenantName.Trim());
                    topic = await SaveTopic(topicRequest.TenantId, topicRequest.TenantName.Trim(), topicArn);
                }
                else
                {
                    topicArn = topic.Arn;
                }

                //Subscribe endpoint to topic 
                string endpointArn = await _awsService.SubscribeToTopic(topicArn, "sms", topicRequest.MobileNumber);
                //save to db
                if (!string.IsNullOrEmpty(endpointArn))
                    await SaveUserSubscription(topicRequest.MobileNumber, endpointArn, topic.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
                return false;
            }
            return true;
        }

        public async Task<UnSubscribeTopicResponse> UnsubscribeEndpointToTopic(UnsubscribeTopicRequest unsubcribeRequest)
        {
            UnSubscribeTopicResponse response = new UnSubscribeTopicResponse();
            try
            {
                TblNotifyUserSubscription userSubscription = null;
                TblNotifyTopic topic = (await _topicRepository.Find(t => t.TenantId == unsubcribeRequest.TenantId)).SingleOrDefault();
                //Get the endpoint subscription arn from db.
                if (topic != null)
                    userSubscription = (await _subscriptionRepository.Find(s => s.UserEndpoint == unsubcribeRequest.MobileNumber && s.TopicId == topic.Id)).SingleOrDefault();
                if (userSubscription == null)
                {
                    response.ErrMessage = "No subscription found for specified endpoint";
                    response.NoArn = true;
                    return response;
                }
                response.Status = await _awsService.UnSubscribeToTopic(userSubscription.SubscriptionArn);
                if (response.Status)
                {
                    //soft delete user subscription from db
                    userSubscription.IsActive = false;
                    await _subscriptionRepository.Update(userSubscription);
                }

            }
            catch (Exception ex)
            {
                response.Status = false;
                response.ErrMessage = "Internal server error";
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return response;
        }

        public async Task<DeleteTopicResponse> DeleteTopic(string tenantId)
        {
            DeleteTopicResponse deleteResponse = new DeleteTopicResponse();
            try
            {
                //Get topic arn from db
                TblNotifyTopic topic = (await _topicRepository.Find(t => t.TenantId == tenantId)).SingleOrDefault();
                if (topic == null)
                {
                    deleteResponse.ErrMessage = "Topic not found for specified tenant";
                    deleteResponse.NoTopic = true;
                    return deleteResponse;
                }
                deleteResponse.DeleteStatus = await _awsService.DeleteTopic(topic.Arn);
                if (deleteResponse.DeleteStatus)
                {
                    topic.IsActive = false;
                    await _topicRepository.Update(topic);
                }

            }
            catch (Exception ex)
            {
                deleteResponse.DeleteStatus = false;
                deleteResponse.ErrMessage = "Internal server error";
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return deleteResponse;
        }

        public async Task<bool> PublishMessageToTopic(TopicMessageRequest messageRequest)
        {
            try
            {
                //get topic arn from db for each tenant and send sms.
                foreach (var tenant in messageRequest.TenantList)
                {
                    TblNotifyTopic topicDetails = (await _topicRepository.Find(tp => tp.TenantId == tenant)).SingleOrDefault();
                    if (topicDetails != null)
                    {
                        var result = await _awsService.PublishTopicMessage(messageRequest.Message, topicDetails.Arn);
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
                return false;
            }
            return true;
        }

        #endregion

        #region Private Methods

        private (string, string) SetOTPMessage(string Template, Dictionary<string, string> placeHolders, bool dummyNumber = false)
        {
            Random rNumber = new Random();
            int OTP = rNumber.Next(1000, 9999);
            if (dummyNumber)
            {
                OTP = 1234;
            }
            if (placeHolders == null)
                placeHolders = new Dictionary<string, string>();
            placeHolders.Add("Otp", OTP.ToString());
            placeHolders.Add("Date", DateTime.Now.Date.ToString("MM/dd/yyyy"));
            string replacedTemplate = Api.Infrastructure.Extensions.StringExtensions.Format(Template, placeHolders);
            return (replacedTemplate, OTP.ToString());
        }

        private async Task<string> LogOtp(string context, string contextID, string OTP)
        {
            TblNotifyOtpTracker notfyOtptracker = new TblNotifyOtpTracker
            {
                Context = context,
                ContextId = contextID,
                Otp = OTP,
                IsActive = true,
                CreatedBy = contextID,
                CreatedDate = DateTime.Now,
                UpdatedBy = contextID,
                UpdatedDate = DateTime.Now,
            };
            var result = await _otpTrackerRepository.Add(notfyOtptracker);
            return result.Id;
        }

        private async Task<TblNotifyTopic> SaveTopic(string tenantId, string topicName, string topicArn)
        {
            TblNotifyTopic notfyTopic = new TblNotifyTopic
            {
                TenantId = tenantId,
                Name = topicName,
                Arn = topicArn,
                IsActive = true,
                //change audit trail later.
                CreatedBy = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now
            };
            var result = await _topicRepository.Add(notfyTopic);
            return result;
        }

        private async Task<bool> SaveUserSubscription(string endpoint, string subscriptonARN, string topicId)
        {
            TblNotifyUserSubscription userSubscription = new TblNotifyUserSubscription
            {
                TopicId = topicId,
                UserEndpoint = endpoint,
                SubscriptionArn = subscriptonARN,
                IsActive = true,
                CreatedBy = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.Now
            };
            var result = await _subscriptionRepository.Add(userSubscription);
            if (result == null)
                return false;
            else
                return true;
        }

        #endregion
    }
}
