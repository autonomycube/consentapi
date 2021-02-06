﻿using Contest.Wallet.Api.Contracts;
using Contest.Wallet.Api.Infrastructure.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Contest.Wallet.Api.Infrastructure.Installers
{
    internal class RegisterHealthChecks : IServiceRegistration
    {
        public void RegisterAppServices(IServiceCollection services, IConfiguration config)
        {
            //Register HealthChecks and UI
            services.AddHealthChecks()
                    .AddCheck("Google Ping", new PingHealthCheck("www.google.com", 100))
                    .AddCheck("Bing Ping", new PingHealthCheck("www.bing.com", 100))
                    .AddMySql(config["ConnectionStrings:DbConnection"],
                            name: "MySQL:DbConnection",
                            failureStatus: HealthStatus.Unhealthy);

            services.AddHealthChecksUI();
        }
    }
}
