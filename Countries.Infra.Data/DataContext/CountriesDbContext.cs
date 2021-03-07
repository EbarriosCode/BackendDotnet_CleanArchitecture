using Countries.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Countries.Infra.Data.DataContext
{
    public class CountriesDbContext : IdentityDbContext
    {
        public CountriesDbContext(DbContextOptions<CountriesDbContext> options)
            : base(options) { }

        public DbSet<WeatherForecast> Forecast { get; set; }
    }
}
