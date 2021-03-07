using Countries.SPA.Middleware;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Countries.SPA
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Authentication and Authorization IdentityServer Configuration
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            // Add IoC
            IoC.AddDependency(builder);

            builder.Services.AddApiAuthorization();
            await builder.Build().RunAsync();
        }
    }
}
