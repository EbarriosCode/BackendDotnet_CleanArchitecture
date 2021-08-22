using Countries.Infra.Data.DataContext;
using Countries.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Infra.Data.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var _context = new CountriesDbContext(serviceProvider.GetRequiredService<DbContextOptions<CountriesDbContext>>()))
            {
                // Add Countries in DB
                if (_context.Countries.Any())                
                    return;
                
                _context.Countries.AddRange(
                    new Country { Name = "Francia", Alpha_2 = "Fr", Alpha_3 = "Fra", NumericCode = "250" },
                    new Country { Name = "Alemania", Alpha_2 = "De", Alpha_3 = "Deu", NumericCode = "276" },
                    new Country { Name = "Guatemala", Alpha_2 = "Gt", Alpha_3 = "Gtm", NumericCode = "320" }
                 );

                _context.SaveChanges();

                // Add Subdivision in Db
                if (_context.Subdivisions.Any())
                    return;

                _context.Subdivisions.AddRange(
                    // Francia
                    new Subdivision { Name = "French Polynesia", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Fr")).CountryID },
                    new Subdivision { Name = "Saint Barthélemy", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Fr")).CountryID },
                    new Subdivision { Name = "Saint Martin", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Fr")).CountryID },
                    new Subdivision { Name = "Saint Pierre and Miquelon", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Fr")).CountryID },
                    new Subdivision { Name = "Wallis and Futuna", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Fr")).CountryID },

                    // Alemania
                    new Subdivision { Name = "Baden-Württemberg", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("De")).CountryID },
                    new Subdivision { Name = "Bavaria (Bayern)", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("De")).CountryID },
                    new Subdivision { Name = "Berlin", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("De")).CountryID },
                    new Subdivision { Name = "Brandenburg", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("De")).CountryID },
                    new Subdivision { Name = "Bremen", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("De")).CountryID },

                    // Guatemala
                    new Subdivision { Name = "Suchitepequez", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Gt")).CountryID },
                    new Subdivision { Name = "San Marcos", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Gt")).CountryID },
                    new Subdivision { Name = "Quetzaltenango", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Gt")).CountryID },
                    new Subdivision { Name = "Huehuetenango", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Gt")).CountryID },
                    new Subdivision { Name = "Izabal", CountryID = _context.Countries.FirstOrDefault(c => c.Alpha_2.Contains("Gt")).CountryID }
                );

                _context.SaveChanges();
            }
        }
    }
}
