using Consent.Api.Contracts;
using Consent.Api.Tenant.DTO.Request;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Consent.Api.Tenant.Infrastructure.Installers
{
    internal class RegisterModelValidators : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration configuration)
        {
            //Register DTO Validators
            services.AddTransient<IValidator<CreateTenantRequest>, CreateTenantRequestValidator>();
            services.AddTransient<IValidator<UpdateTenantRequest>, UpdateTenantRequestValidator>();
            services.AddTransient<IValidator<EmailAddressesRequest>, EmailAddressesRequestValidator>();

            //Disable Automatic Model State Validation built-in to ASP.NET Core
            services.Configure<ApiBehaviorOptions>(opt => { opt.SuppressModelStateInvalidFilter = true; });
        }
    }
}
