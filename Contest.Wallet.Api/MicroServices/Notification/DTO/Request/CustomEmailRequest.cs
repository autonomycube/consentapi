using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Notification.DTO.Request
{
    public class CustomEmailRequest
    {
        public string emailAddress { get; set; }
        public string mailSubject { get; set; }
        public string htmlText { get; set; }
        public List<string> attachmentPath { get; set; }
    }
    public class CustomEmailRequestValidator : AbstractValidator<CustomEmailRequest>
    {
        public CustomEmailRequestValidator()
        {
            RuleFor(o => o.emailAddress).NotEmpty().EmailAddress().WithMessage("Requires a valid Email"); 
            RuleFor(o => o.mailSubject).NotEmpty();
            RuleFor(o => o.htmlText).NotEmpty();
        }
    }
}
