using Consent.Common.Repository.SQL.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Consent.Common.Repository.Extensions.ServiceCollection
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddGenericRepository<TContext>([NotNullAttribute] this IServiceCollection services, Action<DbContextOptionsBuilder> options = null)
             where TContext : DbContext, IDbContext
        {
            services.AddDbContext<TContext>(options);
            services.AddScoped<IDbContext, TContext>();
            return services;
        }
    }
}
