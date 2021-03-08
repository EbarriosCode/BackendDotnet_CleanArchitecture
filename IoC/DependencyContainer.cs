using Application.Interfaces;
using Application.Services;
using Countries.Infra.Data.Repositories.Custom;
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

            // Inject the service Countries            
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();

            return services;
        }
    }
}
