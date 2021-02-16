using FluentValidation;
using System;
using System.Collections.Generic;

namespace Consent.Api.Notification.DTO.Request
{
    public class CreateTmpSmsRequest
    {
        public string MobileNumber { get; set; }
        public string Context { get; set; }
        public string SubContext { get; set; }
        public string ContextId { get; set; }
        public bool IsArabic { get; set; } = false;
        public Dictionary<string,string> PlaceHolders { get; set; }
    }

    public class CreateTmpSmsRequestValidator : AbstractValidator<CreateTmpSmsRequest>
    {
        public CreateTmpSmsRequestValidator()
        {
            RuleFor(o => o.MobileNumber).NotEmpty();
            RuleFor(o => o.Context).NotEmpty();
            RuleFor(o => o.SubContext).NotEmpty();
            RuleFor(o => o.ContextId).NotEmpty();
            RuleFor(o => o.IsArabic).NotNull();
        }
    }
}
