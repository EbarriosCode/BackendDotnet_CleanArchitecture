using Countries.Infra.Data.DataContext;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;

namespace Countries.API
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
            // Add Entity Framework DbContext
            services.AddDbContext<CountriesDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllersWithViews();
            services.AddRazorPages();

            // Indentity Server Configuration
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CountriesDbContext>()
                    .AddDefaultTokenProviders();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Countries.API", Version = "v1" });
            });

            // Init IoC Services
            DependencyContainer.AddDependency(services);
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();            
            bool adminRoleExists = await RoleManager.RoleExistsAsync("Admin");

            if (!adminRoleExists)
                await RoleManager.CreateAsync(new IdentityRole("Admin"));            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Countries.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Create Roles in Database
            CreateRoles(services).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
