using Countries.Models.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Countries.Infra.Data.DataContext
{
    public class CountriesDbContext : IdentityDbContext
    {
        public CountriesDbContext(DbContextOptions<CountriesDbContext> options)
            : base(options) { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }
    }
}
