using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Consent.Api.Infrastructure.Configs;
using Consent.Api.Notification.Services.Abstract;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services
{
    public class AwsService : IAwsService
    {
        private readonly AppConfig _appConfigs;
        public AwsService(IOptions<AppConfig> appConfigs)
        {
            _appConfigs = appConfigs.Value;
        }

        #region SMS
        public async Task<PublishResponse> SendSms(string MobileNumber, string messageContent)
        {
            using (AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                //SetDefaultSmsAttributes(snsClient);
                //SetSMSAttributesResponse setResponse = await snsClient.SetSMSAttributesAsync(SetDefaultSmsAttributes(null));

                var messageAttributes = new Dictionary<string, MessageAttributeValue>();
                var smsType = new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = "Transactional"
                };
                // register sender Id in aws.
                var smsSender = new MessageAttributeValue
                {
                    DataType = "String",
                    StringValue = _appConfigs.SenderID
                };

                messageAttributes.Add("AWS.SNS.SMS.SMSType", smsType);
                messageAttributes.Add("AWS.SNS.SMS.SenderID", smsSender);
                PublishRequest request = new PublishRequest
                {
                    Message = messageContent,
                    PhoneNumber = MobileNumber,
                    MessageAttributes = messageAttributes
                };
                return await snsClient.PublishAsync(request);
            }
        }

        #endregion

        #region Email
        public async Task<SendEmailResponse> SendSingleEmail(string ToEmail, string subject, string htmltext)
        {
            using (var Mailclient = new AmazonSimpleEmailServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = _appConfigs.FromMail,
                    Destination = new Destination
                    {
                        ToAddresses =
                            new List<string> { ToEmail }
                    },
                    Message = new Message
                    {
                        Subject = new Amazon.SimpleEmail.Model.Content(subject),
                        Body = new Body
                        {
                            Html = new Amazon.SimpleEmail.Model.Content
                            {
                                Charset = "UTF-8",
                                Data = htmltext
                            },
                            Text = new Amazon.SimpleEmail.Model.Content
                            {
                                Charset = "UTF-8",
                                Data = ""
                            }
                        }
                    }
                };
                return await Mailclient.SendEmailAsync(sendRequest);
            }

        }

        public async Task<SendEmailResponse> SendMultipleEmail(List<string> receivers, string subject, string htmltext)
        {
            using (var Mailclient = new AmazonSimpleEmailServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = _appConfigs.FromMail,
                    Destination = new Destination
                    {
                        ToAddresses = receivers

                    },
                    Message = new Message
                    {
                        Subject = new Amazon.SimpleEmail.Model.Content(subject),
                        Body = new Body
                        {
                            Html = new Amazon.SimpleEmail.Model.Content
                            {
                                Charset = "UTF-8",
                                Data = htmltext
                            },
                            Text = new Amazon.SimpleEmail.Model.Content
                            {
                                Charset = "UTF-8",
                                Data = ""
                            }
                        }
                    }
                };
                return await Mailclient.SendEmailAsync(sendRequest);
            }

        }

        public async Task<SendRawEmailResponse> SendEmailWithAttachment(List<string> ToEmail, string subject, string htmltext, List<string> attachmentPath)
        {

            //Form message body with attachment
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_appConfigs.FromMail));
            message.Subject = subject;
            message.Body = GetMessageBody(htmltext, attachmentPath).Result.ToMessageBody();

            using (var stream = new MemoryStream())
            {
                message.WriteTo(stream);

                using (var Mailclient = new AmazonSimpleEmailServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
                {
                    var sendRequest = new SendRawEmailRequest
                    {
                        Source = _appConfigs.FromMail,
                        Destinations = ToEmail,
                        RawMessage = new RawMessage(stream)
                    };
                    return await Mailclient.SendRawEmailAsync(sendRequest);
                }
            }

        }

        #endregion

        #region Topic
        public async Task<string> CreateSNSTopic(string topicName)
        {
            string topicARN = string.Empty;
            //**change region later to baharin
            using (AmazonSimpleNotificationServiceClient snsClient = new AmazonSimpleNotificationServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                CreateTopicRequest createTopicRequest = new CreateTopicRequest(topicName);
                CreateTopicResponse createTopicResponse = await snsClient.CreateTopicAsync(createTopicRequest);
                if (createTopicResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    topicARN = createTopicResponse.TopicArn;
                    //Set topic attributes
                    SetTopicAttributesRequest topicAttributesRequest = new SetTopicAttributesRequest();
                    topicAttributesRequest.TopicArn = topicARN;
                    topicAttributesRequest.AttributeName = "DeliveryPolicy";
                    topicAttributesRequest.AttributeValue = SetTopicRetryPolicy();
                    SetTopicAttributesResponse response = await snsClient.SetTopicAttributesAsync(topicAttributesRequest);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {

                    }
                }

            }
            return topicARN;
        }

        public async Task<string> SubscribeToTopic(string topicArn, string protocol, string endpoint)
        {
            string subscriptionArn = string.Empty;
            using (var snsClient = new AmazonSimpleNotificationServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                SubscribeRequest subscribeRequest = new SubscribeRequest(topicArn, protocol, endpoint);
                SubscribeResponse subscribeResponse = await snsClient.SubscribeAsync(subscribeRequest);
                if (subscribeResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    subscriptionArn = subscribeResponse.SubscriptionArn;
            }
            return subscriptionArn;

        }

        public async Task<bool> UnSubscribeToTopic(string subArn)
        {
            using (var snsClient = new AmazonSimpleNotificationServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                UnsubscribeResponse unsubscribeResponse = await snsClient.UnsubscribeAsync(subArn);
                if (unsubscribeResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> DeleteTopic(string topicArn)
        {
            using (var snsClient = new AmazonSimpleNotificationServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                DeleteTopicRequest deleteTopicRequest = new DeleteTopicRequest(topicArn);
                DeleteTopicResponse deleteTopicResponse = await snsClient.DeleteTopicAsync(deleteTopicRequest);
                if (deleteTopicResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> PublishTopicMessage(string Message, string topicARN)
        {

            using (var snsClient = new AmazonSimpleNotificationServiceClient(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1))
            {
                PublishRequest pubRequest = new PublishRequest();
                pubRequest.Message = Message;
                pubRequest.TopicArn = topicARN;

                // add optional MessageAttributes...
                pubRequest.MessageAttributes["AWS.SNS.SMS.SMSType"] =
                   new MessageAttributeValue { StringValue = "Promotional", DataType = "String" };
                pubRequest.MessageAttributes["AWS.SNS.SMS.SenderID"] =
                   new MessageAttributeValue { StringValue = _appConfigs.SenderID, DataType = "String" };
                PublishResponse pubResponse = await snsClient.PublishAsync(pubRequest);
                if (pubResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    return true;
                else
                    return false;
            }

        }

        #endregion

        #region S3 Bucket
        #endregion

        #region Private Methods
        private SetSMSAttributesRequest SetDefaultSmsAttributes(AmazonSimpleNotificationServiceClient snsClient)
        {
            SetSMSAttributesRequest setRequest = new SetSMSAttributesRequest();
            setRequest.Attributes["DefaultSenderID"] = _appConfigs.SenderID;
            setRequest.Attributes["DefaultSMSType"] = "Transactional";
            //setRequest.Attributes["DeliveryStatusSuccessSamplingRate"] = "10";
            //setRequest.Attributes["UsageReportS3Bucket"] = "sns-sms-daily-usage";
            //SetSMSAttributesResponse setResponse = snsClient.SetSMSAttributesAsync(setRequest).Result;    
            return setRequest;
        }

        private string SetTopicRetryPolicy()
        {
            string updatedPolicy = "{\"http\":{\"defaultHealthyRetryPolicy\":{\"minDelayTarget\":20," +
                "\"maxDelayTarget\":30,\"numRetries\":5,\"numMaxDelayRetries\":0,\"numNoDelayRetries\":0,\"numMinDelayRetries\":0,\"backoffFunction\":\"linear\"},\"disableSubscriptionOverrides\":false}}";
            return updatedPolicy;
        }

        private async Task<MemoryStream> ReadObjectDataAsync(string bucketName, string key)
        {

            var client = new AmazonS3Client(_appConfigs.AwsAccessKey, _appConfigs.AwsSecretKey, Amazon.RegionEndpoint.USEast1);
            try
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = key
                };
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                {
                    using (var stream = new MemoryStream())
                    {
                        await response.ResponseStream.CopyToAsync(stream);
                        return stream;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (client != null)
                    client.Dispose();
            }

        }

        private async Task<BodyBuilder> GetMessageBody(string htmlPart, List<string> attachments)
        {
            var body = new BodyBuilder()
            {
                HtmlBody = htmlPart,
                TextBody = string.Empty,
            };

            foreach (string filePath in attachments)
            {
                //Generate s3 bucket details from path
                AmazonS3Uri bucketUri = new AmazonS3Uri(filePath);
                string bucketName = bucketUri.Bucket;
                string filekey = bucketUri.Key;
                string fileName = Path.GetFileName(filePath);
                MemoryStream stream = await ReadObjectDataAsync(bucketName, filekey);
                byte[] imageBytes = stream.ToArray();
                stream.Dispose();

                body.Attachments.Add(fileName, imageBytes);
            }
            return body;
        }
        #endregion
    }
}
