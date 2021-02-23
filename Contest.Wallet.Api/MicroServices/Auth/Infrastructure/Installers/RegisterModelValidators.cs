using Consent.Api.Auth.DTO.Request;
using Consent.Api.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Auth.Infrastructure.Installers
{
    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<GenerateOtpRequest>, GenerateOtpRequestValidator>();
            services.AddTransient<IValidator<EmailConfirmationRequest>, EmailConfirmationRequestValidator>();
            services.AddTransient<IValidator<InviteEmailsRequest>, InviteEmailsRequestValidator>();

            //Disable Automatic Model State Validation built-in to ASP.NET Core
            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
        }
    }
}
