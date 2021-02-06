using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Infrastructure.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Contest.Wallet.Api.Infrastructure.Installers
{
    internal class RegisterSwagger : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register Swagger
            //See: https://www.scottbrady91.com/Identity-Server/ASPNET-Core-Swagger-UI-Authorization-using-IdentityServer4
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Contest.Wallet.Api.Notification ASP.NET Core API", Version = "v1" });

                var IdentityServerUrl = config["ApiResourceBaseUrls:AuthServer"];
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{IdentityServerUrl}/connect/authorize"),
                            Scopes = new Dictionary<string, string> {
                                { "bn_global_service", "bn_global_service" },
                            }
                        }
                    }
                });

                options.OperationFilter<SwaggerAuthorizeCheckOperationFilter>();

                //Adding excluded models

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}
