using Consent.Api.Contracts;
using Consent.Api.Notification.DTO.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Notification.Infrastructure.Installers
{
    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<CreateEmailRequest>, CreateEmailRequestValidator>();
            services.AddTransient<IValidator<CreateTmpEmailRequest>, CreateTmpEmailRequestValidator>();
            services.AddTransient<IValidator<CreateTmpSmsRequest>, CreateTmpSmsRequestValidator>();
            services.AddTransient<IValidator<CustomEmailRequest>, CustomEmailRequestValidator>();
            services.AddTransient<IValidator<CustomSmsRequest>, CustomSmsRequestValidator>();
            services.AddTransient<IValidator<MultipleTmpEmailRequest>, MultipleTmpEmailRequestValidator>();
            services.AddTransient<IValidator<SubscribeTopicRequest>, SubscribeTopicRequestValidator>();
            services.AddTransient<IValidator<UnsubscribeTopicRequest>, UnsubscribeTopicRequestValidator>();
            services.AddTransient<IValidator<VerifyOtpRequest>, VerifyOtpRequestValidator>();

            //Disable Automatic Model State Validation built-in to ASP.NET Core
            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
        }
    }
}
