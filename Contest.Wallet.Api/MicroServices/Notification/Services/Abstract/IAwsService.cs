using Amazon.SimpleEmail.Model;
using Amazon.SimpleNotificationService.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services.Abstract
{
    public interface IAwsService
    {
        Task<PublishResponse> SendSms(string MobileNumber, string messageContent);
        Task<SendEmailResponse> SendSingleEmail(string ToEmail, string subject, string htmltext);
        Task<SendEmailResponse> SendMultipleEmail(List<string> receivers, string subject, string htmltext);
        Task<SendRawEmailResponse> SendEmailWithAttachment(List<string> ToEmail, string subject, string htmltext, List<string> attachmentPath);
        Task<string> CreateSNSTopic(string topicName);
        Task<string> SubscribeToTopic(string topicArn, string protocol, string endpoint);
        Task<bool> UnSubscribeToTopic(string subArn);
        Task<bool> DeleteTopic(string topicArn);
        Task<bool> PublishTopicMessage(string Message, string topicARN);
    }
}
