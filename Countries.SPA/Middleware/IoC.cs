using Countries.SPA.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Countries.SPA.Middleware
{
    public static class IoC
    {
        public static WebAssemblyHostBuilder AddDependency(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped<IForecastService, ForecastService>();

            return builder;
        }
    }
}
