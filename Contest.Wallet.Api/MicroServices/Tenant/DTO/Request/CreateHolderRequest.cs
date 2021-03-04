using FluentValidation;
using System;

namespace Consent.Api.Tenant.DTO.Request
{
    public class CreateHolderRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateHolderRequestValidator : AbstractValidator<CreateHolderRequest>
    {
        public CreateHolderRequestValidator()
        {
            RuleFor(o => o.FirstName).NotNull().NotEmpty();
            RuleFor(o => o.PhoneNumber).NotNull().NotEmpty();
            RuleFor(o => o.Email).NotNull().NotEmpty()
                .EmailAddress().WithMessage("Email is invalid");
        }
    }
}