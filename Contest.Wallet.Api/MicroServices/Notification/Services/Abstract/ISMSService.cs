using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using System;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface ISmsService
    {
        Task<CreateTmpSmsResponse> SendSingleTemplateSMS(CreateTmpSmsRequest smsRequest);
        Task<bool> SendCustomSMS(CustomSmsRequest smsRequest);
        Task<VerifyOtpResponse> VerifyOtp(VerifyOtpRequest otpRequest);
        Task<bool> SubscribeEndpointToTopic(SubscribeTopicRequest topicRequest);
        Task<UnSubscribeTopicResponse> UnsubscribeEndpointToTopic(UnsubscribeTopicRequest unsubcribeRequest);
        Task<DeleteTopicResponse> DeleteTopic(string tenantId);
        Task<bool> PublishMessageToTopic(TopicMessageRequest messageRequest);
    }
}
