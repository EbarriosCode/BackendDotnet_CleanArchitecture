using Countries.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Countries.Infra.Data.DataContext
{
    public class CountriesDbContext : DbContext
    {
        public CountriesDbContext(DbContextOptions<CountriesDbContext> options)
            : base(options) { }

        public DbSet<WeatherForecast> Forecast { get; set; }
    }
}
