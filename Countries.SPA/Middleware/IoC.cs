using Countries.SPA.Services;
using Countries.SPA.StateProvider;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Countries.SPA.Middleware
{
    public static class IoC
    {
        public static WebAssemblyHostBuilder AddDependency(this WebAssemblyHostBuilder builder)
        {
            // Configuración del AuthorizationStateProvider
            builder.Services.AddScoped<JWTAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, JWTAuthStateProvider>(provider => provider.GetRequiredService<JWTAuthStateProvider>());
            builder.Services.AddScoped<ILoginService, JWTAuthStateProvider>(provider => provider.GetRequiredService<JWTAuthStateProvider>());

            // Inject CountryService
            builder.Services.AddScoped<ICountriesHttpService, CountriesHttpService>();
            builder.Services.AddScoped<ISubdivisionsHttpService, SubdivisionsHttpService>();

            return builder;
        }
    }
}
