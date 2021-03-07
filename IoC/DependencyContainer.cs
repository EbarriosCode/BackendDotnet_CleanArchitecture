using Application.Interfaces;
using Application.Services;
using Application.Services.CustomRepositories;
using Countries.Infra.Data.Repositories;
using Countries.Infra.Data.Repositories.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // Inject the service generic repository
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Inject the service JWT
            services.AddTransient<IJWTService, JWTService>();

            services.AddScoped<IForecastService, ForecastService>();
            services.AddScoped<IForecastRepository, ForecastRepository>();

            return services;
        }
    }
}
