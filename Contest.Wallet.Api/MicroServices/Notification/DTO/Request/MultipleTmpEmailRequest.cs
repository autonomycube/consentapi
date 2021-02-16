using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Notification.DTO.Request
{
    public class MultipleTmpEmailRequest
    {
        public List<string> EmailList { get; set; }
        public string Context { get; set; }
        public string SubContext { get; set; }
        public bool IsArabic { get; set; }
        public Dictionary<string, string> PlaceHolders { get; set; }
        public List<string> AttachmentPath { get; set; }
    }

    public class MultipleTmpEmailRequestValidator : AbstractValidator<MultipleTmpEmailRequest>
    {
        public MultipleTmpEmailRequestValidator()
        {
            RuleFor(o => o.EmailList).NotEmpty();
            RuleFor(o => o.Context).NotEmpty();
            RuleFor(o => o.SubContext).NotEmpty();
            RuleFor(o => o.IsArabic).NotNull();
        }
    }
}
