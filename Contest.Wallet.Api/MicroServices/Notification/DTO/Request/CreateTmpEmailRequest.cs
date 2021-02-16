using FluentValidation;
using System.Collections.Generic;

namespace Consent.Api.Notification.DTO.Request
{
    public class CreateTmpEmailRequest
    {
        public string Email { get; set; }
        public string Context { get; set; }
        public string SubContext { get; set; }
        public bool IsArabic { get; set; } = false;
        public Dictionary<string, string> PlaceHolders { get; set; }
        public List<string> AttachmentPath { get; set; }
    }

    public class CreateTmpEmailRequestValidator : AbstractValidator<CreateTmpEmailRequest>
    {
        public CreateTmpEmailRequestValidator()
        {
            RuleFor(o => o.Email).NotEmpty().EmailAddress().WithMessage("Requires a valid Email");
            RuleFor(o => o.Context).NotEmpty();
            RuleFor(o => o.SubContext).NotEmpty();
            RuleFor(o => o.IsArabic).NotNull();
        }
    }
}
