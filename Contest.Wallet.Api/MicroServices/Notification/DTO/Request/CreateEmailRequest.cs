using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Notification.API.v1.DTO.Request
{
    public class CreateEmailRequest
    {
        public string EmailAddress { get; set; }
        public string MailSubject { get; set; }
        public string HtmlText { get; set; }
        public List<string> AttachmentPath { get; set; }
    }

    public class CreateEmailRequestValidator : AbstractValidator<CreateEmailRequest>
    {
        public CreateEmailRequestValidator()
        {
            RuleFor(o => o.EmailAddress).NotEmpty().EmailAddress().WithMessage("Requires a valid Email");
            RuleFor(o => o.MailSubject).NotEmpty();
            RuleFor(o => o.HtmlText).NotEmpty();
        }
    }
}