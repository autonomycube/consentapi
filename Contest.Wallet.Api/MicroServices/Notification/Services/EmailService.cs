using Consent.Api.Notification.Data.Repositories.Abstract;
using Consent.Api.Notification.DTO.Request;
using Consent.Api.Notification.DTO.Response;
using Consent.Api.Notification.Services.Abstract;
using Consent.Common.ApplicationMonitoring.Abstract;
using Consent.Common.EnityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consent.Api.Notification.Services
{
    public class EmailService : IEmailService
    {
        #region Private variables      
        
        private readonly ILogger<EmailService> _logger;
        private readonly IAwsService _awsService;
        private readonly IEmailRepository _emailRepository;

        #endregion

        #region Constructor

        public EmailService(ILogger<EmailService> logger, IAwsService awsService, IEmailRepository emailRepository)
        {
            _logger = logger;
            _awsService = awsService;
            _emailRepository = emailRepository;
        }

        #endregion

        #region Public Methods

        public async Task<CreateTmpEmailResponse> SendSingleTemplateEmail(CreateTmpEmailRequest emailRequest)
        {
            CreateTmpEmailResponse emailResponse = new CreateTmpEmailResponse();
            emailResponse.MailStatus = false;
            emailResponse.ErrMessage = string.Empty;
            emailResponse.NoTemplate = false;
            string emailContent = string.Empty;
            string subject = string.Empty;
            try
            {
                //Get email template from DB based on context & subcontext.
                TblNotifyEmailTemplate emailTemplate = (await _emailRepository.Find(st => st.Context.Trim().ToLower().Equals(emailRequest.Context.Trim().ToLower()) &&
                                                      st.SubContext.Trim().ToLower().Equals(emailRequest.SubContext.Trim().ToLower()))).SingleOrDefault(); 
                if (emailTemplate == null)
                {                    
                    emailResponse.ErrMessage = "No Template found";
                    emailResponse.NoTemplate = true;
                    return emailResponse;
                }
                    
                //Check for language               
                emailContent = emailRequest.IsArabic ? emailTemplate.ArbHtmlText : emailTemplate.HtmlText;
                subject = emailRequest.IsArabic ? emailTemplate.ArbMailSubject : emailTemplate.MailSubject;
                if (string.IsNullOrEmpty(emailContent))
                {
                    emailResponse.ErrMessage = "No Template found";
                    emailResponse.NoTemplate = true;
                    return emailResponse;
                }

                //Replace placeholders
                string replacedTemplate = string.Empty;
                if (emailTemplate.HasPlaceholder)               
                    replacedTemplate = Consent.Api.Infrastructure.Extensions.StringExtensions.Format(emailContent, emailRequest.PlaceHolders);

                //Check for attachment
                if (emailRequest.AttachmentPath != null && emailRequest.AttachmentPath.Count > 0)
                {
                    var rawEmailResponse = await _awsService.SendEmailWithAttachment(new List<string> { emailRequest.Email }, subject, replacedTemplate, emailRequest.AttachmentPath);
                    if (rawEmailResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        emailResponse.MailStatus = true;
                    }
                }
                else
                {
                    var response = await _awsService.SendSingleEmail(emailRequest.Email, subject, replacedTemplate);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        emailResponse.MailStatus = true;
                    }
                }
                                   
                
            }
            catch(Exception ex)
            {
                emailResponse.ErrMessage = "Internal server error";
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
           return emailResponse;
        }

        public async Task<bool> SendCustomEmail(CustomEmailRequest customRequest)
        {
            bool status = false;
            try
            {
                //check for attachment
                if (customRequest.attachmentPath != null && customRequest.attachmentPath.Count > 0)
                {
                    var rawEmailResponse = await _awsService.SendEmailWithAttachment(new List<string> { customRequest.emailAddress }, customRequest.mailSubject, customRequest.htmlText, customRequest.attachmentPath);
                    if (rawEmailResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        status = true;
                    }
                }
                else
                {
                    var response = await _awsService.SendSingleEmail(customRequest.emailAddress, customRequest.mailSubject, customRequest.htmlText);
                    if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        status = true;
                }
                
            }
            catch(Exception ex)
            {
                status = false;
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return status;
        }

        public async Task<CreateTmpEmailResponse> SendMultipleTemplatedEmail(MultipleTmpEmailRequest emailRequest)
        {
            CreateTmpEmailResponse emailResponse = new CreateTmpEmailResponse();           
            string emailContent = string.Empty;
            string subject = string.Empty;
            try
            {
                //Get email template from DB based on context & subcontext.
                TblNotifyEmailTemplate emailTemplate = (await _emailRepository.Find(st => st.Context.Trim().ToLower().Equals(emailRequest.Context.Trim().ToLower()) &&
                                                      st.SubContext.Trim().ToLower().Equals(emailRequest.SubContext.Trim().ToLower()))).SingleOrDefault(); 
                if (emailTemplate == null)
                {
                    emailResponse.ErrMessage = "No Template found";
                    emailResponse.NoTemplate = true;
                    return emailResponse;
                }

                //Check for language               
                emailContent = emailRequest.IsArabic ? emailTemplate.ArbHtmlText : emailTemplate.HtmlText;
                subject = emailRequest.IsArabic ? emailTemplate.ArbMailSubject : emailTemplate.MailSubject;
                if (string.IsNullOrEmpty(emailContent))
                {
                    emailResponse.ErrMessage = "No Template found";
                    emailResponse.NoTemplate = true;
                    return emailResponse;
                }

                //Replace placeholders
                string replacedTemplate = string.Empty;
                if (emailTemplate.HasPlaceholder)
                    replacedTemplate = Api.Infrastructure.Extensions.StringExtensions.Format(emailContent, emailRequest.PlaceHolders);

                //only 50 emails at a time                              
                int receiverCnt = emailRequest.EmailList.Count;
                for (int i = 0; i < receiverCnt; i = i + 50)
                {
                    var receivers = emailRequest.EmailList.Skip(i).Take(50).ToList();
                    if (emailRequest.AttachmentPath != null && emailRequest.AttachmentPath.Count > 0)
                        await _awsService.SendEmailWithAttachment(receivers, subject, replacedTemplate, emailRequest.AttachmentPath);
                    else
                        await _awsService.SendMultipleEmail(receivers, subject, replacedTemplate);
                }
                emailResponse.MailStatus = true;                                
                
            }
            catch (Exception ex)
            {
                emailResponse.ErrMessage = "Internal server error";
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return emailResponse;
        }

        public async Task<bool> SendMultipleCustomEmail(CustomMultiEmailRequest customRequest)
        {
            bool status = false;
            try
            {
                //only 50 emails at a time                
                int receiverCnt = customRequest.EmailList.Count;
                for (int i = 0; i < receiverCnt; i = i + 50)
                {
                    var receivers = customRequest.EmailList.Skip(i).Take(50).ToList();
                    if (customRequest.attachmentPath != null && customRequest.attachmentPath.Count > 0)
                        await _awsService.SendEmailWithAttachment(customRequest.EmailList, customRequest.mailSubject, customRequest.htmlText, customRequest.attachmentPath);
                    else
                        await _awsService.SendMultipleEmail(customRequest.EmailList, customRequest.mailSubject, customRequest.htmlText);
                }
                status = true;                            

            }
            catch (Exception ex)
            {
                status = false;
                _logger.LogError($"ErrorSource: {ex.Source} Error: {ex.ToString()}");
            }
            return status;
        }

        #endregion

        #region Private Methods
        #endregion
    }
}
