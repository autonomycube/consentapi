using AspNetCoreRateLimit;
using AutoMapper;
using AutoWrapper;
using Consent.Api.Data.Configuration;
using Consent.Api.Infrastructure.Configs;
using Consent.Api.Infrastructure.Extensions;
using Consent.Api.Infrastructure.Helpers;
using Consent.Api.Infrastructure.Middlewares;
using Consent.Common.ApplicationMonitoring.CloudWatch.Extensions;
using Consent.Common.ApplicationMonitoring.CloudWatch.Models;
using Consent.Common.EnityFramework.DbContexts;
using Consent.Common.EnityFramework.Entities.Identity;
using Consent.Common.Payment.Paytm;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using AuthMappingProfileConfiguration = Consent.Api.Auth.Infrastructure.Configs.MappingProfileConfiguration;
using NotificationMappingProfileConfiguration = Consent.Api.Notification.Infrastructure.Configs.MappingProfileConfiguration;
using PaymentMappingProfileConfiguration = Consent.Api.Payment.Infrastructure.Configs.MappingProfileConfiguration;
using TenantMappingProfileConfiguration = Consent.Api.Tenant.Infrastructure.Configs.MappingProfileConfiguration;
using UtilityMappingProfileConfiguration = Consent.Api.Utility.Infrastructure.Configs.MappingProfileConfiguration;

namespace Consent.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add cloudwatch logger
            services.AddCloudWatchLogger(new CloudWatchConfiguration(Configuration));

            //Register services in Installers folder
            services.AddServicesInAssembly(Configuration);

            //Register MVC/Web API, NewtonsoftJson and add FluentValidation Support
            services.AddControllers()
                    .AddJsonOptions(ops => ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                    .AddNewtonsoftJson(ops => { ops.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; })
                    .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; });

            // Register DB Contexts
            services.AddDbContexts(Configuration);

            // Register 
            services.AddAuthenticationServices<ConsentIdentityDbContext, UserIdentity, UserIdentityRole>(Configuration);

            //Register Automapper
            services.AddAutoMapper(typeof(AuthMappingProfileConfiguration));
            services.AddAutoMapper(typeof(NotificationMappingProfileConfiguration));
            services.AddAutoMapper(typeof(PaymentMappingProfileConfiguration));
            services.AddAutoMapper(typeof(TenantMappingProfileConfiguration));
            services.AddAutoMapper(typeof(UtilityMappingProfileConfiguration));

            // Register Health Checks
            services.AddHealthChecks();

            // Add HttpContext Accessor Middleware
            services.AddHttpContextAccessor();

            services.AddPaytmPaymentGateway(Configuration);

            // configure strongly typed settings objects  
            services.Configure<AppConfig>(Configuration.GetSection(nameof(AppConfig)));
            services.Configure<NotificationSeedData>(Configuration.GetSection(nameof(NotificationSeedData)));
            services.Configure<TenantSeedData>(Configuration.GetSection(nameof(TenantSeedData)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Added for safari OPTIONS missing issue
            app.UseOptions();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //Enable Swagger and SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId("all_swagger_ui");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Consent API v1");
            });

            //Enable HealthChecks and UI
            app.UseHealthChecks("/selfcheck", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).UseHealthChecksUI(setup =>
            {
                setup.AddCustomStylesheet($"{env.ContentRootPath}/Infrastructure/HealthChecks/Ux/branding.css");
            });

            //Enable AutoWrapper.Core
            //More info see: https://github.com/proudmonkey/AutoWrapper
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { IsDebug = true });

            //Enable AspNetCoreRateLimit
            app.UseIpRateLimiting();

            app.UseRouting();

            //Enable CORS
            app.UseCors("AllowAll");

            //Adds authenticaton middleware to the pipeline so authentication will be performed automatically on each request to host
            app.UseAuthentication();

            //Adds authorization middleware to the pipeline to make sure the Api endpoint cannot be accessed by anonymous clients
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
